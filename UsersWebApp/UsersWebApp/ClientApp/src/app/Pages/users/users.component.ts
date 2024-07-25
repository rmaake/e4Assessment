import { Component, OnInit } from '@angular/core';
import { UserService } from '../../Services/users/user.service';
import { User } from '../../Models/user';
import { Route, Router } from '@angular/router';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {
  users: User[] = [];
  msgStr: string ='';
  isLoading: boolean = false;
  constructor(private service: UserService, private router: Router) {

  }

  ngOnInit(): void {
    this.service.users.subscribe(users => this.users = users);
    this.service.isLoading.subscribe(val => {
      this.isLoading = val;
    });
    this.service.msg.subscribe(val => {
      this.msgStr = val
    });
  }
  
  deleteUser(userId: number) {
    this.service.deleteUser(userId);
  }

  modifyUser(userId: number) {
    this.router.navigate(['home', userId]);
  }

}
