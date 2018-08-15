import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TagsService {
  readonly rootUrl = "http://localhost:7068";

  constructor(private http: HttpClient) { }

  GetAllTags(){
    var url = `${this.rootUrl}/api/tags`;
    return this.http.get(url);
  }
}