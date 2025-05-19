import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AppComponent } from 'src/app/app.component';
import { CardCursoComponent } from 'src/app/component/card-curso/card-curso.component';
import { Curso } from 'src/app/models/curso';
import { AuthService } from 'src/app/service/Auth.service';
import { CursoService } from 'src/app/service/curso.service';
import dayjs from 'dayjs';

@Component({
  selector: 'app-cursos',
  imports: [CommonModule,CardCursoComponent],
  templateUrl: './cursos.component.html',
  styleUrls:['./cursos.component.css']
})
export class CursosComponent {
cursosList:Curso[]=[];
    constructor(private cursoService: CursoService, private router: Router, private auth: AppComponent) {
      
      if(localStorage.getItem('token')){
        cursoService.CursosLogin().then((cursos)=>{
          this.cursosList=cursos

          this.cursosList.forEach((item) => {
          item.fechaInicio = dayjs(item.fechaInicio).format('DD MMM')
          item.fechaFin = dayjs(item.fechaFin).format('DD MMM')
     })
        }).catch((err) => {
          alert('Vuelve a iniciar sesion')
          auth.logout()
        })
      }else{
      cursoService.Cursos().then((cursos)=>{
        this.cursosList=cursos
        this.cursosList.forEach((item) => {
        item.fechaInicio = dayjs(item.fechaInicio).format('DD MMM')
        item.fechaFin = dayjs(item.fechaFin).format('DD MMM')
     })
      })
     }
     
  }
    
}
