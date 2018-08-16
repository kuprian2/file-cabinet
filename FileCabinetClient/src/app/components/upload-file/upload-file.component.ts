import { Component, OnInit } from '@angular/core';
import { FileService } from '../../services/file.service';
import { TagService } from '../../services/tag.service';
import { TagInfo } from '../../models/tag-info';
import { Observable } from 'rxjs';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-upload-file',
  templateUrl: './upload-file.component.html',
  styleUrls: ['./upload-file.component.css']
})
export class UploadFileComponent implements OnInit {
  file;
  fileToUpload: File;
  fileName: string;
  fileDescription: string;
  fileTags: string;

  constructor(
    private fileService: FileService,
    private tagServcie: TagService
  ) { }

  ngOnInit() {
    this.resetForm();
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
  }

  resetForm(form?: NgForm){
    if(form != null){
      form.reset();
    }
    this.fileName = "";
    this.fileDescription = "";
    this.fileTags = "";
  }

  onSubmit(form: NgForm) {
    this.tagServcie.GetAllTags()
    .subscribe((allTags: TagInfo[]) => {
      var inputTags = this.fileTags.split(" ").filter(str => str !== "");
      
      var parsedTags = inputTags.map(inputTag => allTags.find(tag => tag.Name == inputTag));

      if(!parsedTags.includes(undefined)){
        this.fileService.UploadFile(this.fileName, this.fileDescription, parsedTags, this.fileToUpload)
        .subscribe((data: any) => {
          this.resetForm(form);
        });
      }
    });
  }
}