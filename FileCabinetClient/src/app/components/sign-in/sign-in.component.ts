import { Component, OnInit } from '@angular/core';
import { UserLogin } from '../../models/user-login';
import { NgForm } from '@angular/forms';
import { UserService } from '../../services/user.service';
import { Router } from '@angular/router';
import { UserInfo } from '../../models/user-info';
import { CommonService } from '../../services/common.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  user: UserLogin;

  constructor(
    private userService: UserService,
    private router: Router,
    private commonService: CommonService
  ) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm) {
    if(form != null) form.reset();
    this.user = {
      Email: "",
      Password: ""
    }
  }

  onSubmit(form: NgForm) {
    this.userService.AuthenticateUser(form.value)
    .subscribe((data: any) =>{
      localStorage.setItem("userToken", data.access_token);
      this.userService.GetUserInfo()
      .subscribe((userInfo: UserInfo) => {
        this.commonService.SetLocalUserData(userInfo)
        this.resetForm(form);
        this.router.navigate([""]);
      });
    });
  }
}
