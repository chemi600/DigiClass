import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { LoginComponent } from './pages/login/login.component';
import { PrincipalComponent } from './pages/principal/prinicpal.component';
import { MisCursosComponent } from './pages/mis-cursos/mis-cursos.component';
import { CursosComponent } from './pages/cursos/cursos.component';
import { DetallesComponent } from './pages/detalles/detalles.component';
import { AboutUsComponent } from './pages/about-us/about-us.component';

const routeConfig: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    title: 'DigiClass',
  },
  {
    path: '',
    component: PrincipalComponent,
    title: 'DigiClass',
  },
  {
    path: 'misCursos',
    component: MisCursosComponent,
    title: 'Mis Cursos'
  },
  {
    path: 'cursos',
    component: CursosComponent,
    title: 'DigiClass'
  },
  {
    path: 'detalles/:id',
    component: DetallesComponent,
    title: 'DigiClass',
  },
  {
    path: 'about',
    component: AboutUsComponent,
    title: 'DigiClass',
  },
  {
    path: '**',
    component: PageNotFoundComponent,
  },
];

export default routeConfig;
