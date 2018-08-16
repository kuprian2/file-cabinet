import { Component, OnInit } from '@angular/core';
import { FileInfo } from '../../models/file-info';
import { FileInfoService } from '../../services/file-info.service';
import { CommonService } from '../../services/common.service';

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.css']
})
export class FilesComponent implements OnInit {
  files: FileInfo[];

  constructor(
    private fileInfoService: FileInfoService,
    private commonService: CommonService
  ) { }

  ngOnInit() {
    this.fetchFiles();
  }

  fetchFiles(keyword?: string, tags?: string[]) {
    var observableFiles;

    if(keyword != null && keyword != ""){
      observableFiles = this.fileInfoService.GetFiles(keyword);
    }
    else if(tags != null){
      observableFiles = this.fileInfoService.GetFilesByTags(tags);
    }
    else{
      observableFiles = this.fileInfoService.GetFiles();
    }

    observableFiles
    .subscribe((data: FileInfo[]) => {
      this.files = data;
      this.files.forEach((file => {
        file.SizeInBytes = this.commonService.FormatFileSize(+file.SizeInBytes);
        file.UploadDate = this.commonService.FormatDate(file.UploadDate);
      }))
    });
  }

  onSearch(searchString: string) {
    var keywords = searchString.trim().split(" ");
    if(keywords.length == 0){
      return;
    }

    if(keywords.length == 1){
      this.fetchFiles(keywords[0]);
      return;
    }

    this.fetchFiles(null, keywords);
  }
}
