import { Injectable } from '@angular/core';
import { CreateCurso } from '../models/create-curso';
import { Curso, Result } from '../models/curso';

@Injectable({
  providedIn: 'root'
})
export class CursoService {
  readonly baseUrl = 'http://localhost:5072/api';
  private pathCurso = `${this.baseUrl}/Curso`;

  private token: string | null = null;

  constructor() { 
    this.token = localStorage.getItem('authToken');

  }

  async CrearCurso(Data: CreateCurso): Promise<boolean> {
    const response=await fetch(this.pathCurso, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(Data)
    }).catch(error => {
      throw new Error(error)
    });

    if (!response.ok) {
      throw new Error('Algo salio mal')
    }else{
      return true
    }
      
    }

    async Cursos(): Promise<[Curso]> {
      const response=await fetch(this.pathCurso, {
        method: 'GET',
        headers: { 'Content-Type': 'application/json' },
      }).then((data)=>data.json())
      .catch(error => {
        throw new Error(error)
      });
  
      
        return response;
      
        
      }
    
    
}
