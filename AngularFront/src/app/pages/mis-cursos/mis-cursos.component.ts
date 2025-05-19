import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { AppComponent } from 'src/app/app.component';
import { CardCursoComponent } from 'src/app/component/card-curso/card-curso.component';
import { CrearCursoComponent } from 'src/app/component/crear-curso/crear-curso.component';
import { Curso } from 'src/app/models/curso';
import { CursoService } from 'src/app/service/curso.service';

@Component({
  selector: 'app-mis-cursos',
  imports: [CommonModule,CrearCursoComponent,CardCursoComponent],
  templateUrl:'./mis-cursos.component.html',
  styleUrls: ['./mis-cursos.component.css']
})
export class MisCursosComponent {
  showModal = false;
  role=localStorage.getItem('role')
  cursosActuales:Curso[]=[]

  constructor(private cursoService: CursoService, private auth: AppComponent){
    this.role=localStorage.getItem('role')
    cursoService.MisCursos().then((cursos)=>this.cursosActuales=cursos).catch((err) => {
          alert('Vuelve a iniciar sesion')
          auth.logout()
        })
  }
  
  
  openModal(){
    this.showModal=true;
  }

  closeModal() {
    this.showModal = false;
  }
}
