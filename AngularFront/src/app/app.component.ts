import {Component} from '@angular/core';
import { RouterModule } from '@angular/router';
import { LoginComponent } from './pages/login/login.component';
import { Router } from '@angular/router';


@Component({
  selector: 'app-root',
  imports: [RouterModule],
  standalone: true,
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'Comercio';

  constructor(private router: Router) {}

  goToRegister() {
    console.log("hola")
    this.router.navigate(['/login']);
  }
}


