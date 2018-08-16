import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { FileInfo } from '../models/file-info';

@Injectable({
  providedIn: 'root'
})
export class FileInfoService {
  readonly rootUrl = "http://localhost:7068";

  constructor(private http: HttpClient) { }

  GetFiles(keyword?: string) : Observable<FileInfo[]>{
    var url = `${this.rootUrl}/api/files`;
    if(keyword != null){
      url = `${url}?keyword=${keyword}`;
    }

    return this.http.get<FileInfo[]>(url);
  }

  GetFilesByTags(tags: string[]) : Observable<FileInfo[]>{
    var searchParams = `?tags=${tags.join(",")}`
    const url = `${this.rootUrl}/api/files${searchParams}`;
    
    return this.http.get<FileInfo[]>(url);
  }
}
