import { Component, OnInit } from '@angular/core';
import { TagCreate } from '../../models/tag-create';
import { NgForm } from '@angular/forms';
import { TagsService } from '../../services/tags.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-tag-create',
  templateUrl: './tag-create.component.html',
  styleUrls: ['./tag-create.component.css']
})
export class TagCreateComponent implements OnInit {
  tag: TagCreate;

  constructor(
    private tagsService: TagsService,
    private router: Router
  ) { }

  ngOnInit() {
    this.resetForm();
  }

  resetForm(form?: NgForm){
    if(form != null) form.reset();
    this.tag = {
      Name: ""
    }
  }

  onSubmit(form: NgForm){
    this.tagsService.CreateTag(form.value)
    .subscribe((data: any) => {
      if(data.Succeeded == true)
        this.resetForm(form);
        this.router.navigate(["/tags"]);
    });
  }
}
