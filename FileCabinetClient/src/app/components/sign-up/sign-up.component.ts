import { Component, OnInit } from '@angular/core';
import { UserRegister } from '../../models/user-register';
import { NgForm, FormsModule } from '@angular/forms';
import { UserService } from '../services/user.service';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {
  user: UserRegister;
  
  constructor(private userService: UserService) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm){
    if(form != null) form.reset();
    this.user = {
      Email: "",
      Password: "",
      ConfirmPassword: ""
    }
  }

  onSubmit(form: NgForm){
    console.log(form.value);
    this.userService.registerUser(form.value)
    .subscribe((data: any) => {
      console.log(data);
      if(data.Succeeded == true)
        this.resetForm(form);
    });
  }
}
