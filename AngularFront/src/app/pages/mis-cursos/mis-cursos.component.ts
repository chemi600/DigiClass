import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { CrearCursoComponent } from 'src/app/component/crear-curso/crear-curso.component';

@Component({
  selector: 'app-mis-cursos',
  imports: [CommonModule,CrearCursoComponent],
  templateUrl:'./mis-cursos.component.html',
  styleUrls: ['./mis-cursos.component.css']
})
export class MisCursosComponent {
  showModal = false;
  
  openModal(){
    this.showModal=true;
  }

  closeModal() {
    this.showModal = false;
  }
}
