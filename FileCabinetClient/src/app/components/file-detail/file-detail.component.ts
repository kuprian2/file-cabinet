import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { FileInfoService } from '../../services/file-info.service';
import { FileInfo } from '../../models/file-info';
import { CommonService } from '../../services/common.service';

@Component({
  selector: 'app-file-detail',
  templateUrl: './file-detail.component.html',
  styleUrls: ['./file-detail.component.css']
})
export class FileDetailComponent implements OnInit {
  file: FileInfo;

  constructor(
    private route: ActivatedRoute,
    private location: Location,
    private fileInfoService: FileInfoService,
    private commonService: CommonService
  ) { }

  ngOnInit() {
    this.getFile();
  }

  getFile() {
    const id = +this.route.snapshot.paramMap.get('id');
    this.fileInfoService.GetFile(id)
    .subscribe(file => this.file = {
      Id: file.Id,
      Name: file.Name,
      Description: file.Description,
      SizeInBytes: this.commonService.FormatFileSize(+file.SizeInBytes),
      UploadDate: this.commonService.FormatDate(file.UploadDate),
      Tags: file.Tags
    });
  }

  goBack() {
    this.location.back();
  }

  userAuthorized() {
    return this.commonService.UserAuthorized();
  }
}
