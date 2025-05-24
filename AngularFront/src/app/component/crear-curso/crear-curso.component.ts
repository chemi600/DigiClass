import { CommonModule } from '@angular/common';
import { Component, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CreateCurso } from 'src/app/models/create-curso';
import { CursoService } from 'src/app/service/curso.service';

@Component({
  selector: 'app-crear-curso',
  imports: [CommonModule,FormsModule],
  templateUrl: './crear-curso.component.html',
  styleUrls:['./crear-curso.component.css'] 
})
export class CrearCursoComponent {
@Output() close = new EventEmitter<void>();
curso:CreateCurso={titulo:'',descripcion:'',fechaFin:new Date(),fechaInicio:new Date()}
  fechaMinima: Date = new Date()
  fechaString: string
  constructor(private cursoService: CursoService) {
    const año = this.fechaMinima.getFullYear();
    const mes = String(this.fechaMinima.getMonth() + 1).padStart(2, '0');
    const dia = String(this.fechaMinima.getDate() + 1).padStart(2, '0');
    
    this.fechaString = `${año}-${mes}-${dia}`
  }

  onClose() {
    this.close.emit();
  }

  async onSubmit() {
    try{

    
    if(await this.cursoService.CrearCurso(this.curso)){
      alert('Curso creado exitosamente')
      window.location.reload()
    }
    
    }catch(error:any){
      alert(error.message);
  }
    this.onClose();
  }
}
