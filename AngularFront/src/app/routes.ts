import { Routes } from '@angular/router';
import { PageNotFoundComponent } from './pages/page-not-found/page-not-found.component';
import { LoginComponent } from './pages/login/login.component';
import { RegisterComponent } from './pages/register/register.component';
import { PrincipalComponent } from './pages/principal/prinicpal.component';
import { PropuestaPageComponent } from './pages/propuesta-page/propuesta-page.component';
import { AnadirComponent } from './pages/anadir/anadir.component';
import { MisCursosComponent } from './pages/mis-cursos/mis-cursos.component';
import { CursosComponent } from './pages/cursos/cursos.component';
import { DetallesComponent } from './pages/detalles/detalles.component';

const routeConfig: Routes = [
  {
    path: 'login',
    component: LoginComponent,
    title: 'I.E.S. Comercio',
  },
  {
    path: 'registro',
    component: RegisterComponent,
    title: 'I.E.S. Comercio',
  },
  {
    path: '',
    component: PrincipalComponent,
    title: 'I.E.S. Comercio',
  },
  {
    path: 'propuestaPage/:id',
    component: PropuestaPageComponent,
    title: 'I.E.S. Comercio'
  },
  {
    path: 'misCursos',
    component: MisCursosComponent,
    title: 'Mis Cursos'
  },
  {
    path: 'anadir',
    component: AnadirComponent,
    title: 'I.E.S. Comercio'
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
    path: '**',
    component: PageNotFoundComponent,
  },
];

export default routeConfig;
