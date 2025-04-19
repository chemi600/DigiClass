import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { CardCursoComponent } from 'src/app/component/card-curso/card-curso.component';
import { Curso } from 'src/app/models/curso';
import { CursoService } from 'src/app/service/curso.service';

@Component({
  selector: 'app-cursos',
  imports: [CommonModule,CardCursoComponent],
  templateUrl: './cursos.component.html',
  styleUrls:['./cursos.component.css']
})
export class CursosComponent {
cursosList:Curso[]=[];
    constructor(private cursoService: CursoService) {
      cursoService.Cursos().then((cursos)=>{
        this.cursosList=cursos
      })
    }
  
    
}
