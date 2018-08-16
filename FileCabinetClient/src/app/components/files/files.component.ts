import { Component, OnInit } from '@angular/core';
import { FileInfo } from '../../models/file-info';
import { FileInfoService } from '../../services/file-info.service';
import { TagInfo } from '../../models/tag-info';

@Component({
  selector: 'app-files',
  templateUrl: './files.component.html',
  styleUrls: ['./files.component.css']
})
export class FilesComponent implements OnInit {
  files: FileInfo[];

  constructor(private fileInfoService: FileInfoService) { }

  ngOnInit() {
    this.fetchFiles();
  }

  fetchFiles(keyword?: string, tags?: string[]){
    var observableFiles;

    if(keyword != null){
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
        file.SizeInBytes = this.getFileSize(+file.SizeInBytes);
        file.UploadDate = new Date(Date.parse(file.UploadDate)).toLocaleString();
      }))
    });
  }

  onSearch(searchString: string){
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

  getFileSize(sizeInBytes: number){
    var i = -1;
    var byteUnits = [' kB', ' MB', ' GB', ' TB', 'PB', 'EB', 'ZB', 'YB'];
    do {
      sizeInBytes = sizeInBytes / 1024;
        i++;
    } while (sizeInBytes > 1024);

    return Math.max(sizeInBytes, 0.1).toFixed(2) + byteUnits[i];
  }
}
