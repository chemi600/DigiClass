import {Component} from '@angular/core';
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




  
  constructor(private router: Router) {
    this.token=localStorage.getItem('token')
  }

  goToRegister() {
    
    this.router.navigate(['/login']);
  }
}


