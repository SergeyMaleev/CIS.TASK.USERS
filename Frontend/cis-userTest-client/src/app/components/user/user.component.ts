import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from 'src/app/models/role';
import { User } from 'src/app/models/user';
import { AuthService } from 'src/app/services/auth.service';
import { UsersService } from 'src/app/services/users.service';

@Component({
  selector: 'app-user',
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.scss']
})
export class UserComponent implements OnInit {

  public currentUser: User = new User();
  public roles!: Role[];
  public urlPath = '';

  constructor(private usersService: UsersService,
    private router: Router,
    private route: ActivatedRoute,
    private as: AuthService) { }

  ngOnInit(): void {
    this.urlPath = this.route.snapshot.paramMap.get('id') ?? '';
    this.usersService
      .getAvailableRoles()
      .subscribe(res => {
        this.roles = res;
      });

    if (this.urlPath !== 'add')
    {
      this.usersService
        .getUser(this.urlPath)
        .subscribe(res => {
          this.currentUser = res;
        });
    }
  }

  isCheckedRole(role: Role): boolean{
    return this.currentUser.roles?.find(x => x.id === role.id) != null;
  }

  roleChange(user: User , role: Role): void{
    if (user.roles.find(x => x.id === role.id ) == null)
    {
      this.usersService
        .addRole(user.id, role.id)
        .subscribe(res => {
          user.roles = res;
        });
    }else{
      this.usersService
        .deleteRole(user.id, role.id)
        .subscribe(res => {
          user.roles = res;
        });
    }
  }

  Save(): void{
    if (this.urlPath !== 'add') {
      this.usersService
        .updateUser(this.urlPath, this.currentUser)
        .subscribe(res => {
          this.currentUser = res;
          alert('Изменения внесены');
        });
    }
    else
      {
      this.usersService
        .addUser(this.currentUser)
        .subscribe(res => {
          this.currentUser = res;
          alert('Пользователь создан');
        });
    }
    this.router.navigate(['']);
  }

  Exit(): void {
    this.router.navigate(['']);
  }
}
