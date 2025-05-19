import {Component,HostListener} from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';



@Component({
  selector: 'app-root',
  imports: [RouterModule,CommonModule],
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'DigiClass';
  token=localStorage.getItem('token')
  name=localStorage.getItem('name')
  role=localStorage.getItem('role')

  mostrarDesplegable = false;


  
  constructor(private router: Router) {
    this.token=localStorage.getItem('token')
  }

  toggleDesplegable() {
    this.mostrarDesplegable = !this.mostrarDesplegable;
  }

  goToRegister() {
    
    this.router.navigate(['/login']);
  }

  setToken(){
    this.token=localStorage.getItem('token')
  }

  setName(){
    this.name=localStorage.getItem('name')
  }

  setRole(){
    this.role=localStorage.getItem('role')
  }

  @HostListener('document:click', ['$event'])
  onClickFuera(event: MouseEvent) {
    const objetivo = event.target as HTMLElement;
    if (!objetivo.closest('.contenedor')) {
      this.mostrarDesplegable = false;
    }
  }

  logout(){
    this.token=''
    this.role=''
    this.name=''

    localStorage.removeItem('token')
    localStorage.removeItem('name')
    localStorage.removeItem('role')

    //window.location.reload();
    this.router.navigate(['login'])
  }
}


