import {Inject, Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {API_URL} from '../app-injection-tokens';
import {Observable} from 'rxjs';
import {User} from '../models/user';
import {Role} from "../models/role";

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private baseApiUrl = `${this.apiUrl}`;

  constructor(private http: HttpClient, @Inject(API_URL) private apiUrl: string) { }

  getUsers(): Observable<User[]>{
    return this.http.get<User[]>(`${this.baseApiUrl}/Users`);
  }

  getUser(id: string): Observable<User>{
    return this.http.get<User>(`${this.baseApiUrl}/Users/${id}`);
  }

  updateUser(id: string, user: User | undefined): Observable<User>  {
    return this.http.put<User>(`${this.baseApiUrl}/Users/${id}`, user);
  }

  addUser(user: User): Observable<User>  {
    return this.http.post<User>(`${this.baseApiUrl}/Users/`, user);
  }

  deleteUser(id: string): Observable<boolean>  {
    return this.http.delete<boolean>(`${this.baseApiUrl}/Users/${id}`);
  }

  getAvailableRoles(): Observable<Role[]> {
    return this.http.get<Role[]>(`${this.baseApiUrl}/Users/getAvailableRoles`);
  }

  addRole(userId: number, roleId: number): Observable<Role[]> {
    return this.http.get<Role[]>(`${this.baseApiUrl}/Users/addRole/${userId}/${roleId}`);
  }

  deleteRole(userId: number, roleId: number): Observable<Role[]>{
    return this.http.get<Role[]>(`${this.baseApiUrl}/Users/deleteRole/${userId}/${roleId}`);
  }
}
