import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/main-menu/auth/login.component';
import { RegisterComponent } from './components/main-menu/auth/register.component';
import { MainMenuComponent } from './components/main-menu/main-menu.component';
import { AuthGuard } from './services/auth.guard';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'main-menu', component: MainMenuComponent, canActivate: [AuthGuard]  },
  // Добавьте другие маршруты, если необходимо
  // { path: 'other', component: OtherComponent },
  // { path: 'profile', component: ProfileComponent },
  // и так далее
  { path: '', redirectTo: '/main-menu', pathMatch: 'full' }, // Перенаправление на /login при пустом пути
  { path: '**', redirectTo: '/main-menu' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
