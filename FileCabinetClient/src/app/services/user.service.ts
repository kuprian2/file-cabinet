import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { UserRegister } from '../models/user-register';
import { UserLogin } from '../models/user-login';


@Injectable({
  providedIn: 'root'
})
export class UserService {
  readonly rootUrl = "http://localhost:7068";

  constructor(private http: HttpClient) { }

  registerUser(user: UserRegister){
    const url = `${this.rootUrl}/api/account/register`;
    const body: UserRegister = {
      Email: user.Email,
      Password: user.Password,
      ConfirmPassword: user.ConfirmPassword
    };
    return this.http.post(url, body);
  }

  authenticateUser(user: UserLogin){
    const url = `${this.rootUrl}/token`;
    const body = new HttpParams()
    .set("grant_type", "password")
    .set("username", user.Email)
    .set("password", user.Password);
    var header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

    return this.http.post(url, body.toString(), { headers: header });
  }

  getUserInfo(){
    const url = `${this.rootUrl}/api/account/userinfo`;
    var header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

    return this.http.get(url, { headers: header });
  }
}
