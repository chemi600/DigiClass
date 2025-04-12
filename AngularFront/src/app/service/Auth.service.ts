import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { LoginDTO } from '../models/loginDTO';
import { RegistroDTO } from '../models/registroDTO';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  readonly baseUrl = 'https://localhost:7777/api/users';
  private loginUrl = `${this.baseUrl}/login`;
  private registerUrl = `${this.baseUrl}/register`;
  private token: string | null = null;

  constructor() {
    // Recuperar el token desde localStorage al inicializar el servicio
    this.token = localStorage.getItem('authToken');
  }

  login(credentials: LoginDTO): Observable<any> {
    return new Observable<any>(observer => {
      fetch(this.loginUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(credentials)
      })
      .then(response => response.json())
      .then(data => {
        console.log('Login response:', data);
        if (data?.result?.token) {
          this.setToken(data.result.token);
        } else {
          console.warn('⚠️ No se recibió un token válido:', data);
        }
        observer.next(data); // 🔥 Asegura que el Observable emita un valor
        observer.complete(); // 🛑 Finaliza el Observable
      })
      .catch(error => {
        observer.error(error);
      });
    });
  }  

  async register(registroDto: RegistroDTO): Promise<boolean> {
      const response=await fetch(this.registerUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(registroDto)
      })

      if (!response.ok) {
        console.error('Login failed');
        return false;
      }else{
        alert('Registro exitoso')
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