import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Curso } from 'src/app/models/curso';
import { CursoService } from 'src/app/service/curso.service';
import dayjs from 'dayjs';
import { User } from 'src/app/models/user';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDeleteComponent } from 'src/app/component/confirm-delete/confirm-delete.component';
import { AppComponent } from 'src/app/app.component';
import { ConfirmDeleteCursoComponent } from 'src/app/component/confirm-delete-curso/confirm-delete.component';
import { ConfirmDeleteInscripcionComponent } from 'src/app/component/confirm-delete-inscripcion/confirm-delete.component';


@Component({
  selector: 'app-detalles',
  imports: [CommonModule],
  templateUrl:'./detalles.component.html',
  styleUrl:'./detallescss.component.css'
})
export class DetallesComponent {
  Curso:Curso={id:0,createdDate:new Date(),descripcion:'',titulo:'',idProfesor:'',fechaFin:new Date(),fechaInicio:new Date(),nombreProfesor:''};
  route: ActivatedRoute = inject(ActivatedRoute);
  fechaInicio:string=''
  fechaFin:string=''
  role=localStorage.getItem('role')
  activeTab: string = 'tab1';
  participantes:User[]=[]
  status: number = 0 

  constructor(private cursoService: CursoService,private dialog: MatDialog, private auth: AppComponent, private router: Router) {
    this.role=localStorage.getItem('role');
    const token = localStorage.getItem('token')
    //this.status = localStorage.getItem('status')
    const cursoId = parseInt(this.route.snapshot.params['id']);
        cursoService.Curso(cursoId).then((cursos)=>{
          this.Curso=cursos
          this.fechaInicio=dayjs(this.Curso.fechaInicio).format('DD/MM/YYYY')
          this.fechaFin=dayjs(this.Curso.fechaFin).format('DD/MM/YYYY')
        })
        if(token){
          this.cursoService.Check(cursoId, token).then((num)=>this.status=num)
        }
        if(this.role=='profesor')
          this.cursoService.Participantes(cursoId).then((estudiantes)=>{
            this.participantes=estudiantes
        }).catch((err) => {
          alert('Vuelve a iniciar sesion')
          this.auth.logout()
        })
      }
      Inscribirse(){
          this.cursoService.InscribirseCurso(this.Curso.id).catch((err) => {
          alert('Vuelve a iniciar sesion')
          this.auth.logout()
        }).then(() => {
          alert('Inscrito correctamente')
          this.router.navigate(['/misCursos'])
        })
      }
      setTab(tab: string) {
        this.activeTab = tab;
      }

      deleteUser(username: string,userId:string) {
        const dialogRef = this.dialog.open(ConfirmDeleteComponent, {
          width: '300px',
          data: { username, userId }
        });

        dialogRef.afterClosed().subscribe(result => {
          if (result) {
            this.cursoService.DeleteEstudent(this.Curso.id,userId).then((value)=>{
              if(value)
                this.participantes = this.participantes.filter(user => user.id !== userId);
            })

          }
        });

      }

      Borrar(){
        if(this.role == 'profesor'){
          const dialogRef = this.dialog.open(ConfirmDeleteCursoComponent, {
          width: '300px',
          data: null
        });

         dialogRef.afterClosed().subscribe(result => {
          if(result) {
              this.cursoService.BorrarCurso(this.Curso.id).catch((err)=>{
              alert('Vuelve a iniciar sesion')
              this.auth.logout()
          }).then((datos)=>{
              if(datos){
                alert('Curso borrado correctamente')
                this.router.navigate(['/misCursos'])
              }else{
                alert('Ocurrio un error')
              }
            })
          }
         })
          
        } else if(this.role=='estudiante'){
             const dialogRef = this.dialog.open(ConfirmDeleteInscripcionComponent, {
          width: '300px',
          data: null
        });

         dialogRef.afterClosed().subscribe(result => {
          if(result) {
              this.cursoService.DeleteEstudent(this.Curso.id,"").catch((err)=>{
              alert('Vuelve a iniciar sesion')
              this.auth.logout()
          }).then((datos)=>{
              if(datos){
                alert('Has abandonado el curso correctamente')
                this.router.navigate(['/misCursos'])
              }
            })
          }
         })
        }
      }
}
