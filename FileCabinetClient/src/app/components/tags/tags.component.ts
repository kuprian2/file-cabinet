import { Component, OnInit } from '@angular/core';
import { TagService } from '../../services/tag.service';
import { TagInfo } from '../../models/tag-info';
import { CommonService } from '../../services/common.service';

@Component({
  selector: 'app-tags',
  templateUrl: './tags.component.html',
  styleUrls: ['./tags.component.css']
})
export class TagsComponent implements OnInit {
  tags: TagInfo[];

  constructor(
    private tagService: TagService,
    private commonService: CommonService
  ) { }

  ngOnInit() {
    this.tagService.GetAllTags()
    .subscribe((data: TagInfo[]) => {
      this.tags = data;
    })
  }

  userAuthorized() {
    return this.commonService.UserAuthorized();
  }
}
