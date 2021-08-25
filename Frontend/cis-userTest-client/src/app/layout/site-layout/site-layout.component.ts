import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-site-layout',
  templateUrl: './site-layout.component.html',
  styleUrls: ['./site-layout.component.scss']
})
export class SiteLayoutComponent implements OnInit {

  constructor(private as: AuthService, private us: UsersService, private router: Router) { }


  public get IsLoggetIn(): boolean{
    return this.as.isAuthenticated();
  }

  Logout(): void {
    this.as.logout();
  }

  addUser(): void
  {
    this.router.navigate(['users/add']);
  }

  GoHome(): void
  {
    this.router.navigate(['']);
  }


  ngOnInit(): void {
  }

}
