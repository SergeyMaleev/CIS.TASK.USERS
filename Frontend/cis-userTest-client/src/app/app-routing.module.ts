import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { UsersComponent } from './components/users/users.component';
import { AuthGuard } from '../app/guard/auth.guard';
import { SiteLayoutComponent } from './layout/site-layout/site-layout.component';
import { UserComponent } from './components/user/user.component';

const routes: Routes = [
  {path: '', component: SiteLayoutComponent, children: [
    {path: '', redirectTo: '/users', pathMatch: 'full'},
    {path: 'login', component: LoginComponent},
    {path: 'users', component: UsersComponent, canActivate: [AuthGuard]},
    { path: 'users/:id', component: UserComponent, canActivate: [AuthGuard] },
  ]}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
