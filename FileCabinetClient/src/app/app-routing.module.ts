import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { FilesComponent } from './components/files/files.component'
import { TagsComponent } from './components/tags/tags.component'
import { SignInComponent } from './components/sign-in/sign-in.component'
import { SignUpComponent } from './components/sign-up/sign-up.component'
import { TagCreateComponent } from './components/tag-create/tag-create.component'
import { FileDetailComponent } from './components/file-detail/file-detail.component'
import { UploadFileComponent } from './components/upload-file/upload-file.component'
import { EditFileComponent } from './components/edit-file/edit-file.component'

const routes: Routes = [
  { path: '', redirectTo: '/files', pathMatch: 'full' },
  { path: 'files', component: FilesComponent },
  { path: 'files/:id', component: FileDetailComponent },
  { path: 'editfile/:id', component: EditFileComponent },
  { path: 'tags', component: TagsComponent },
  { path: 'newtag', component: TagCreateComponent },
  { path: 'signin', component: SignInComponent },
  { path: 'signup', component: SignUpComponent },
  { path: 'upload', component: UploadFileComponent }];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
