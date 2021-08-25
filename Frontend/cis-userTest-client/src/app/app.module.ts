import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { JwtModule } from '@auth0/angular-jwt';
import { environment } from 'src/environments/environment';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ACCESS_TOKEN_KEY } from './services/auth.service';
import { LoginComponent } from './components/login/login.component';
import { SiteLayoutComponent } from './layout/site-layout/site-layout.component';
import { UsersComponent } from './components/users/users.component';
import { HttpClientModule } from '@angular/common/http';
import { API_URL } from './app-injection-tokens';
import { UserComponent } from './components/user/user.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

export function tokenGetter(): string|null {
  return localStorage.getItem(ACCESS_TOKEN_KEY);
}
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SiteLayoutComponent,
    UsersComponent,
    UserComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    AppRoutingModule,
    JwtModule.forRoot({
      config: {
        tokenGetter,
        allowedDomains: environment.allowedRoutes,
        disallowedRoutes: environment.disallowedRoutes
      }
    }),
    HttpClientModule
  ],
  providers: [{
    provide: API_URL,
    useValue: environment.apiUrl
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
