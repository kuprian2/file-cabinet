import { Injectable } from '@angular/core';
import { UserInfo } from '../models/user-info';

@Injectable({
  providedIn: 'root'
})
export class CommonService {

  constructor() { }

  FormatFileSize(sizeInBytes: number) : string {
    var i = -1;
    var byteUnits = [' kB', ' MB', ' GB', ' TB', 'PB', 'EB', 'ZB', 'YB'];
    do {
      sizeInBytes = sizeInBytes / 1024;
        i++;
    } while (sizeInBytes > 1024);

    return Math.max(sizeInBytes, 0.01).toFixed(2) + byteUnits[i];
  }

  FormatDate(dateInString: string) : string {
    return new Date(Date.parse(dateInString)).toLocaleString();
  }

  UserAuthorized() : boolean {
    return localStorage.getItem("userToken") == null;
  }

  RemoveLocalUserData() : void {
    localStorage.removeItem("userToken");
    localStorage.removeItem("userId");
    localStorage.removeItem("userEmail");
  }

  SetLocalUserData(userInfo: UserInfo) {
    localStorage.setItem("userId", userInfo.Id.toString());
    localStorage.setItem("userEmail", userInfo.Email);
  }
}
