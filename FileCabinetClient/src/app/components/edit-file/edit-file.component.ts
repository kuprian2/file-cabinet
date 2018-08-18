import { Component, OnInit } from '@angular/core';
import { FileService } from '../../services/file.service';
import { TagService } from '../../services/tag.service';
import { TagInfo } from '../../models/tag-info';
import { NgForm } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { FileInfoService } from '../../services/file-info.service';

@Component({
  selector: 'app-edit-file',
  templateUrl: './edit-file.component.html',
  styleUrls: ['./edit-file.component.css']
})
export class EditFileComponent implements OnInit {
  fileToUpload: File;
  fileName: string;
  fileDescription: string;
  fileTags: string;
  id: number;

  constructor(
    private route: ActivatedRoute,
    private fileService: FileService,
    private fileInfoService: FileInfoService,
    private tagServcie: TagService,
    private router: Router
  ) { }

  ngOnInit() {
    this.id = +this.route.snapshot.paramMap.get('id');
    this.resetForm();
    this.getFile();
  }

  handleFileInput(file: FileList) {
    this.fileToUpload = file.item(0);
  }

  getFile(){
    this.fileInfoService.GetFile(this.id)
    .subscribe(file => {
      this.fileName = file.Name,
      this.fileTags = file.Tags.map(tag => tag.Name).join(" "),
      this.fileDescription = file.Description
    });
  }

  resetForm(form?: NgForm){
    if(form != null){
      form.reset();
    }
  }

  onSubmit(form: NgForm) {
    this.tagServcie.GetAllTags()
    .subscribe((allTags: TagInfo[]) => {
      var inputTags = this.fileTags.split(" ").filter(str => str !== "");
      
      var parsedTags = inputTags.map(inputTag => allTags.find(tag => tag.Name == inputTag));

      if(parsedTags.includes(undefined)) return;
      
      this.fileService.EditFile(this.id, this.fileName, this.fileDescription, parsedTags, this.fileToUpload)
      .subscribe(() => {
        this.resetForm(form);
        this.router.navigate([`/files/${this.id}`]);
      });
    });
  }
}
