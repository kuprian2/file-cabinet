import { Component, OnInit } from '@angular/core';
import { TagsService } from '../../services/tags.service';
import { TagInfo } from '../../models/tag-info';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.css']
})
export class TagsComponent implements OnInit {
  tags: TagInfo[];

  constructor(private tagsService: TagsService) { }

  ngOnInit() {
    this.tagsService.GetAllTags()
    .subscribe((data: TagInfo[]) => {
      this.tags = data;
    })
  }

  userAuthorized(){
    return localStorage.getItem("userToken") != null;
  }
}
