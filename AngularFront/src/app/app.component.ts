import { Component, HostListener } from '@angular/core';
import { RouterModule, Router } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterModule, CommonModule],
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'DigiClass';
  token: string | null = localStorage.getItem('token');
  name: string | null = localStorage.getItem('name');
  role: string | null = localStorage.getItem('role');
  menuAbierto: boolean = false;
  mostrarDesplegable: boolean = false;

  constructor(private router: Router) {
    // No es necesario volver a asignar el token aquí, ya se inicializa en la declaración
  }

  toggleDesplegable() {
    this.mostrarDesplegable = !this.mostrarDesplegable;
  }

  toggleMenu() {
    this.menuAbierto = !this.menuAbierto;
    // Cerrar el desplegable del usuario si el menú hamburguesa se cierra
    if (!this.menuAbierto) {
      this.mostrarDesplegable = false;
    }
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

  logout() {
    this.token = null;
    this.name = null;
    this.role = null;
    localStorage.removeItem('token');
    localStorage.removeItem('name');
    localStorage.removeItem('role');
    this.mostrarDesplegable = false;
    this.menuAbierto = false; // Cerrar el menú hamburguesa al cerrar sesión
    this.router.navigate(['/login']);
  }

  @HostListener('document:click', ['$event'])
  onClickFuera(event: MouseEvent) {
    const objetivo = event.target as HTMLElement;
    if (!objetivo.closest('.contenedor') && !objetivo.closest('.menu-toggle')) {
      this.mostrarDesplegable = false;
      // Opcional: cerrar el menú hamburguesa si se hace clic fuera
      this.menuAbierto = false;
    }
  }
}