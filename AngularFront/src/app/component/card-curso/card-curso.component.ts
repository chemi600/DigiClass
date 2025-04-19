import { CommonModule } from '@angular/common';
import { Component, Input } from '@angular/core';
import { RouterModule } from '@angular/router';
import { Curso } from 'src/app/models/curso';

@Component({
  selector: 'app-card-curso',
  imports: [CommonModule,RouterModule],
  templateUrl: './card-curso.component.html',
  styleUrls:['./card-curso.component.css']
})
export class CardCursoComponent {
/*CursoModel:Curso;

constructor(curso:Curso){
  this.CursoModel=curso
}*/

@Input() CursoModel!: Curso;
}
