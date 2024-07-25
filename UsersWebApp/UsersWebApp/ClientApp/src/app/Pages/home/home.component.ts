import { Component, OnInit } from '@angular/core';
import { UserService } from '../../Services/users/user.service';
import { User } from '../../Models/user';
import { ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  user: User;
  phoneNumber: string;
  msgStr: string ='';
  isLoading: boolean = false;
  userForm: FormGroup;
  constructor(private service: UserService, private route: ActivatedRoute, private formBuilder: FormBuilder,) {
   this.user = new User();
   this.phoneNumber = '';
   this.userForm = this.formBuilder.group({
    firstName: ['', {validators: [Validators.required, Validators.maxLength(250), Validators.minLength(3)]} ],
    lastName: ['', {validators: [Validators.required, Validators.maxLength(250), Validators.minLength(3)]}],
    phoneNumber: ['', {validators: [Validators.required, Validators.maxLength(10), Validators.minLength(10)]}]
  });
  
  }

  ngOnInit(): void {
    

    var userId = this.route.snapshot.params['id'];
    if (userId != undefined) {
      this.service.getUser(Number(userId));
    }
    this.service.user.subscribe(user => {
      this.user = user;
    });
    this.service.isLoading.subscribe(val => {
      this.isLoading = val;
    });
    this.service.msg.subscribe(val => {
      this.msgStr = val
    });
  }

  get form() {
    return this.userForm.controls;
  }

  addPhoneNumber() {
    if (this.phoneNumber == '' || this.phoneNumber == undefined || this.phoneNumber.length != 10) {
      this.msgStr = 'Phone number needs to have 10 characters'
      return;
    }
    if (!this.user.phoneNumbers.find(opt=>opt.number == this.phoneNumber)) {
      this.user.phoneNumbers.push({number: this.phoneNumber});
    }
    this.phoneNumber = '';
  }

  deletePhoneNumber(phoneNumber: string) {
    this.user.phoneNumbers = this.user.phoneNumbers.filter(opt=>opt.number != phoneNumber)
  }

  saveUser() {
    this.service.saveUser(this.user);
  }

}
