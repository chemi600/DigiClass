import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Curso } from 'src/app/models/curso';
import { CursoService } from 'src/app/service/curso.service';

@Component({
  selector: 'app-detalles',
  imports: [CommonModule],
  templateUrl:'./detalles.component.html',
  styleUrl:'./detalles.component.css'
})
export class DetallesComponent {
  Curso:Curso={id:0,createdDate:new Date(),descripcion:'',titulo:'',idProfesor:'',fechaFin:new Date(),fechaInicio:new Date()};
  route: ActivatedRoute = inject(ActivatedRoute);
  constructor(private cursoService: CursoService) {
    const cursoId = parseInt(this.route.snapshot.params['id']);
        cursoService.Curso(cursoId).then((cursos)=>{
          this.Curso=cursos
        })
      }
}
