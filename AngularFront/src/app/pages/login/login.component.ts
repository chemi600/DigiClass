import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../service/Auth.service';
import { LoginDTO } from '../../models/loginDTO';
import { firstValueFrom } from 'rxjs';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RegistroDTO } from 'src/app/models/registroDTO';

@Component({
  selector: 'app-login',
  imports: [FormsModule, CommonModule],
  standalone: true,
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  name:string='';
  email: string = '';
  password: string = '';
  repeat_password:string='';
  role:string='Alumno';

  constructor(private authService: AuthService, private router: Router) {}

  async login() {
    const loginDto: LoginDTO = {
      email: this.email, 
      password: this.password,
      token: ''
    };
    try {
      const response = await firstValueFrom(this.authService.login(loginDto));
      if (response?.result?.token) {
        localStorage.setItem('token', response.result.token);
        this.router.navigate(['/principal']);
      } else {
        alert('Error: Usuario o contraseña incorrectos.');
      }      
    } catch (error: any) {
      alert(error.message);
    }
  }

  goToRegister() {
    this.router.navigate(['/registro']);
  }

  toggleForm(formType:string) {
    const loginForm = document.getElementById('loginForm');
    const registroForm = document.getElementById('registroForm');
    const tabs = document.querySelectorAll('.tab');
    
    if(loginForm && registroForm){
    if (formType === 'login') {
      loginForm.classList.add('active');
      registroForm.classList.remove('active');
      tabs[0].classList.add('active');
      tabs[1].classList.remove('active');
    } else {
      registroForm.classList.add('active');
      loginForm.classList.remove('active');
      tabs[1].classList.add('active');
      tabs[0].classList.remove('active');
    }
  }
}

  async register(){
    if (!this.name || !this.email || !this.password || !this.repeat_password || !this.role) {
      alert('Todos los campos son obligatorios.');
      return;
    }

    if (this.password !== this.repeat_password) {
      alert('Las contraseñas no coinciden.');
      return;
    }

    const registroDto: RegistroDTO = {
          name: this.name,
          userName: this.name,
          email: this.email,
          password: this.password,
          role: this.role
        };

        try {
          if(await this.authService.register(registroDto)){
            alert('Usuario registrado con éxito');
            this.router.navigate(['/']);
          }
        } catch (error: any) {
          alert(error.message);
        }
  }
}