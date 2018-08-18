import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { TagInfo } from '../models/tag-info';

@Injectable({
  providedIn: 'root'
})
export class FileService {
  readonly rootUrl = "http://localhost:7068";

  constructor(private http: HttpClient) { }

  UploadFile(fileName: string, fileDescription: string, fileTags: TagInfo[], fileToUpload: File) {
    const url = `${this.rootUrl}/api/upload`;
    var header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

    const formData: FormData = new FormData();
    formData.append("file", fileToUpload, fileToUpload.name);
    formData.append("fileName", fileName);
    formData.append("fileDescription", fileDescription);
    formData.append("fileTagsIds", fileTags.map(tag => tag.Id).join(","));

    return this.http.post(url, formData, { headers : header});
  }

  EditFile(id: number, fileName: string, fileDescription: string, fileTags: TagInfo[], fileToUpload?: File) {
    const url = `${this.rootUrl}/api/upload/${id}`;
    var header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

    const formData: FormData = new FormData();
    if(fileToUpload != null) formData.append("file", fileToUpload, fileToUpload.name);
    formData.append("fileName", fileName);
    formData.append("fileDescription", fileDescription);
    formData.append("fileTagsIds", fileTags.map(tag => tag.Id).join(","));

    return this.http.put(url, formData, { headers : header });
  }

  DeleteFile(id: number) {
    const url = `${this.rootUrl}/api/files/${id}`;
    var header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

    return this.http.delete(url, { headers : header });
  }

  DownloadFile(id: number) {
    const url = `${this.rootUrl}/api/download/${id}`;
    var header = new HttpHeaders()
    .set("Authorization", `Bearer ${localStorage.getItem("userToken")}`);

    return this.http.get(url, { headers : header, responseType : "blob", reportProgress : true });
  }
}
