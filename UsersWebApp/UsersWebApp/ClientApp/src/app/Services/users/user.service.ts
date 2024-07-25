import { Injectable, Inject } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { User } from '../../Models/user';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private baseUrl: string;
  private usersSubject: BehaviorSubject<User[]>;
  private userSubject: BehaviorSubject<User>;
  private msgSubject: BehaviorSubject<string>;
  public msg: Observable<string>;
  private isLoadingSubject: BehaviorSubject<boolean>;
  public isLoading: Observable<boolean>;
  public users: Observable<User[]>;
  public user: Observable<User>;

  constructor(private http: HttpClient, @Inject('BASE_URL') baseUrl: string, private router: Router) {
    this.baseUrl = baseUrl;
    this.usersSubject = new BehaviorSubject<User[]>([]);
    this.userSubject = new BehaviorSubject<User>(new User());
    this.isLoadingSubject = new BehaviorSubject<boolean>(false);
    this.msgSubject = new BehaviorSubject<string>('');
    this.msg = this.msgSubject.asObservable();
    this.isLoading = this.isLoadingSubject.asObservable();
    this.user = this.userSubject.asObservable();
    this.users = this.usersSubject.asObservable();
    this.getUsers();
   }

   getUsers() {
    this.isLoadingSubject.next(true);
    this.msgSubject.next('Getting users...');
    this.http.get(this.baseUrl + 'api/users').subscribe(result => {
      this.usersSubject.next(result as User[]);
      this.isLoadingSubject.next(false);
      this.msgSubject.next('');
    }, error => this.msgSubject.next('Error: Something went wrong'));
   }

   getUser(userId: number) {
    this.isLoadingSubject.next(true);
    this.msgSubject.next(`Getting user with Id=${userId}...`);
    this.http.get(this.baseUrl + `api/users/${userId}`).subscribe(result => {
      this.userSubject.next(result as User);
      this.isLoadingSubject.next(false);
      this.msgSubject.next('');
    }, error => this.msgSubject.next('Error: Something went wrong'));
   }

   deleteUser(userId: number) {
    this.isLoadingSubject.next(true);
    this.msgSubject.next(`Deleting user with Id=${userId}...`);
    this.http.delete(this.baseUrl + `api/users/${userId}`).subscribe(result => {
      this.getUsers();
      this.isLoadingSubject.next(false);
      this.msgSubject.next(`Delete successful`);
    }, error => this.msgSubject.next('Error: Something went wrong'));
   }

   saveUser(user: User) {
    this.isLoadingSubject.next(true);
    this.msgSubject.next(`Saving user details...`);
    this.http.post(this.baseUrl + 'api/users', user).subscribe(result => {
      this.getUsers();
      this.msgSubject.next(`User details saved.`);
      this.isLoadingSubject.next(false);
      this.router.navigate(['users']);
      if (user.userId == 0) {
        this.userSubject.next(new User())
      }
    }, error=> this.msgSubject.next('Error: Something went wrong'));
   }
}
