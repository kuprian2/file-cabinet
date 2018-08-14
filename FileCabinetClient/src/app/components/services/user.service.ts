import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Response } from '@angular/http';
import { Observable } from 'rxjs';
import { UserRegister } from '../../models/user-register';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly rooturl = "http://localhost:7068";

  constructor(private http: HttpClient) { }

  registerUser(user: UserRegister){
    const body: UserRegister = {
      Email: user.Email,
      Password: user.Password,
      ConfirmPassword: user.ConfirmPassword
    };
    return this.http.post(this.rooturl + "/api/account/register", body);
  }
}
