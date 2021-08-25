import { Inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { tap } from 'rxjs/operators';
import { Token } from '../models/token';
import { HttpClient } from '@angular/common/http';
import { API_URL } from '../app-injection-tokens';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';

export const ACCESS_TOKEN_KEY = 'access_token_key';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private http: HttpClient,
    @Inject(API_URL) private apiUrl: string,
    private jwtHelper: JwtHelperService,
    private router: Router
  ) { }

  login(email: string, password: string): Observable<Token>  {
    return this.http
      .post<Token>(`${this.apiUrl}/Auth/login/`, { email, password })
      .pipe(
        tap(token => {
          console.log(token);
          localStorage.setItem(ACCESS_TOKEN_KEY, token.access_token);
          this.router.navigate(['/users']);
        })
      );
  }

  isAuthenticated(): boolean
  {
    const token = localStorage.getItem(ACCESS_TOKEN_KEY);
    return token != null && !this.jwtHelper.isTokenExpired(token);
  }

  isAdmin(): boolean
  {
    const token = localStorage.getItem(ACCESS_TOKEN_KEY);
    const payload = this.jwtHelper.decodeToken(token as string);
    return payload.role === 'Администратор';
  }

  logout(): void
  {
    localStorage.removeItem(ACCESS_TOKEN_KEY);
    this.router.navigate(['/login']);
  }
}


