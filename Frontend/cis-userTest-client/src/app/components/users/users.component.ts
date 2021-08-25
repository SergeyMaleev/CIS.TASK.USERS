import { JsonPipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { pipe } from 'rxjs';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.scss']
})
export class UsersComponent implements OnInit {

  users: User[] = [];


  constructor(private as: AuthService, private us: UsersService, private router: Router) { }


  public get IsLoggetIn(): boolean{
    return this.as.isAuthenticated();
  }

  getUser(id: string): void{
    this.router.navigate(['users', id]);
  }

  deleteUser(id: string): void{
    this.us.deleteUser(id)
      .subscribe(res => {
        alert('Deleted');
        this.ngOnInit();
      }, (error) => {
        alert('error');
    });
  }

  Logout(): void {
    this.as.logout();
  }
  ngOnInit(): void {
    this.us.getUsers()
    .subscribe(res => {
      this.users = res;
    });
  }
}
