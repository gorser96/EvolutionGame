import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { AuthGuard } from './auth/services/auth.guard';
import { MenuComponent } from './components/menu/menu.component';
import { GameListComponent } from './components/game-list/game-list.component';
import { AboutComponent } from './components/about/about.component';
import { RulesComponent } from './components/rules/rules.component';

const routes: Routes = [
  { path: 'game-list', component: GameListComponent, canActivate: [AuthGuard] },
  { path: 'rules', component: RulesComponent, canActivate: [AuthGuard] },
  { path: 'about', component: AboutComponent, canActivate: [AuthGuard] },
  { path: 'menu', component: MenuComponent },
  { path: '', redirectTo: '/menu', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
