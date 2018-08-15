import { Component, OnInit } from '@angular/core';
import { UserLogin } from '../../models/user-login';
import { NgForm } from '@angular/forms';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  user: UserLogin;

  constructor(private userService: UserService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm){
    if(form != null) form.reset();
    this.user = {
      Email: "",
      Password: ""
    }
  }

  onSubmit(form: NgForm){
    this.userService.authenticateUser(form.value)
    .subscribe((data: any) =>{
      localStorage.setItem("userToken", data.access_token);
      this.userService.getUserInfo()
      .subscribe((userInfo: any) => {
        localStorage.setItem("userId", userInfo.Id);
        localStorage.setItem("userEmail", userInfo.Email);
        this.resetForm(form);
      });
    });
  }
}
