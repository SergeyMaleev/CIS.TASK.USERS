import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  constructor(private as: AuthService) { }

  login(email: string, password: string): void{
    this.as.login(email, password)
      .subscribe(
        res => {},
        error => {
          alert(error.message);
        });
  }

  ngOnInit(): void {
  }

}
