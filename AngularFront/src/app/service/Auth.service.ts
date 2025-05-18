import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginDTO } from '../models/loginDTO';
import { RegistroDTO } from '../models/registroDTO';
import { Result, UserDTO } from '../models/userDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly baseUrl = 'http://localhost:5072/api/users';
  private loginUrl = `${this.baseUrl}/login`;
  private registerUrl = `${this.baseUrl}/register`;
  private token: string | null = null;

  constructor() {
    this.token = localStorage.getItem('token');
  }

  async login(credentials: LoginDTO): Promise<Result> {
     const responseFetch=await fetch(this.loginUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
      })
      .then(response => response.json())
      .catch(error => {
        throw new Error(error)
      });

      
        //poconsole.log('Login response:', responseFetch);
        if (responseFetch?.result?.token) {
          const response:Result={
            token: responseFetch.result.token,
            name:responseFetch.result.name,
            role:responseFetch.result.role
          }
          return response
        } else {
          throw new Error('Token no valido')
        }
    
  }  

  async register(registroDto: RegistroDTO): Promise<boolean> {
      const response=await fetch(this.registerUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(registroDto)
      })

      if (!response.ok) {
        console.error('Register failed');
        return false;
      }else{
        return true
      }

    
  }
  
  setToken(token: string): void {
    this.token = token;
    localStorage.setItem('authToken', token);
  }

  getToken(): string | null {
    return this.token || localStorage.getItem('authToken');
  }

  logout(): void {
    this.token = null;
    localStorage.removeItem('authToken');
  }
  
}