import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { TagCreate } from '../models/tag-create';

@Injectable({
  providedIn: 'root'
})
export class TagsService {
  readonly rootUrl = "http://localhost:7068";

  constructor(private http: HttpClient) { }

  GetAllTags(){
    const url = `${this.rootUrl}/api/tags`;
    return this.http.get(url);
  }

  CreateTag(tag:  TagCreate){
    const url = `${this.rootUrl}/api/tags`;
    const body = new HttpParams()
    .set("Name", tag.Name);
    const header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);
    
    return this.http.post(url, body, { headers : header });
  }
}