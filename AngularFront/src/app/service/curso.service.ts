import { Injectable } from '@angular/core';
import { CreateCurso } from '../models/create-curso';
import { Curso, Result } from '../models/curso';
import { User } from '../models/user';

@Injectable({
  providedIn: 'root'
})
export class CursoService {
  readonly baseUrl = 'http://localhost:5072/api';
  private pathCurso = `${this.baseUrl}/Curso`;
  private pathCursoLogin = `${this.baseUrl}/Curso/Nuevos`;

  private misCursos=`http://localhost:5072/api/users/MisCursos`
  private participantes=`http://localhost:5072/api/users/Participantes`
  private Cursoparticipantes=`http://localhost:5072/api/Curso/Participantes`


  private inscribirse=`http://localhost:5072/api/users/Apuntarse`


  private token: string | null = null;

  constructor() { 
    this.token = localStorage.getItem('token');

  }

  async CrearCurso(Data: CreateCurso): Promise<boolean> {
    this.token = localStorage.getItem('token');
    const response=await fetch(this.pathCurso, {
      method: 'POST',
      headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${this.token}` },
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

      async MisCursos(): Promise<[Curso]> {
    this.token = localStorage.getItem('token');
        const response=await fetch(this.misCursos, {
          method: 'GET',
          headers: { 'Content-Type': 'application/json','Authorization': `Bearer ${this.token}` },
        }).then((data)=>data.json())
        .catch(error => {
          throw new Error(error)
        });
          return response;
        }

      async CursosLogin(): Promise<[Curso]> {
    this.token = localStorage.getItem('token');

        const response=await fetch(this.pathCursoLogin, {
          method: 'GET',
          headers: { 'Content-Type': 'application/json', 'Authorization': `Bearer ${this.token}` },
        }).then((data)=>data.json())
        .catch(error => {
          throw new Error(error)
        });
          return response;
        }

      async Curso(id:number): Promise<Curso> {
    this.token = localStorage.getItem('token');

        const response=await fetch(this.pathCurso+`/${id}`, {
          method: 'GET',
          headers: { 'Authorization': `Bearer ${this.token}`},
        }).then((data)=>data.json())
        .catch(error => {
          throw new Error(error)
        });
          return response;
        }

        async InscribirseCurso(Id: number): Promise<boolean> {
        this.token = localStorage.getItem('token');

          const response=await fetch(this.inscribirse+`/${Id}`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json','Authorization': `Bearer ${this.token}` }
          }).catch(error => {
            throw new Error(error)
          });
      
          if (!response.ok) {
            throw new Error('Algo salio mal')
          }else{
            return true
          }
            
          }

          async DeleteEstudent(cursoId: number,userId:string): Promise<boolean> {
            this.token = localStorage.getItem('token');
    
              const response=await fetch(this.Cursoparticipantes+`/${cursoId}`, {
                method: 'DELETE',
                headers: { 'Content-Type': 'application/json','Authorization': `Bearer ${this.token}` },
                body: JSON.stringify(userId)
              }).catch(error => {
                throw new Error(error)
              });
          
              if (!response.ok) {
                throw new Error('Algo salio mal')
              }else{
                return true
              }
                
              }

          async Participantes(id:number): Promise<[User]> {
            this.token = localStorage.getItem('token');
                const response=await fetch(this.participantes+`/${id}`, {
                  method: 'GET',
                  headers: { 'Authorization': `Bearer ${this.token}`},
                }).then((data)=>data.json())
                .catch(error => {
                  throw new Error(error)
                });
                  return response;
                }
    
    
}
