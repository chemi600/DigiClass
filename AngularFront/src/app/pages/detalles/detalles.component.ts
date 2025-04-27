import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Curso } from 'src/app/models/curso';
import { CursoService } from 'src/app/service/curso.service';
import dayjs from 'dayjs';


@Component({
  selector: 'app-detalles',
  imports: [CommonModule],
  templateUrl:'./detalles.component.html',
  styleUrl:'./detalles.component.css'
})
export class DetallesComponent {
  Curso:Curso={id:0,createdDate:new Date(),descripcion:'',titulo:'',idProfesor:'',fechaFin:new Date(),fechaInicio:new Date(),nombreProfesor:''};
  route: ActivatedRoute = inject(ActivatedRoute);
  fechaInicio:string=''
  fechaFin:string=''
  token=localStorage.getItem('token')

  constructor(private cursoService: CursoService) {
    this.token=localStorage.getItem('token');
    const cursoId = parseInt(this.route.snapshot.params['id']);
        cursoService.Curso(cursoId).then((cursos)=>{
          this.Curso=cursos
          this.fechaInicio=dayjs(this.Curso.fechaInicio).format('DD/MM/YYYY')
          this.fechaFin=dayjs(this.Curso.fechaFin).format('DD/MM/YYYY')
        })
      }
      Inscribirse(){
          this.cursoService.InscribirseCurso(this.Curso.id)
      }
}
