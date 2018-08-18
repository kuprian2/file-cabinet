import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FilesComponent } from './components/files/files.component';
import { TagsComponent } from './components/tags/tags.component';
import { SignInComponent } from './components/sign-in/sign-in.component';
import { SignUpComponent } from './components/sign-up/sign-up.component';
import { UserService } from './services/user.service';
import { HttpClientModule } from '../../node_modules/@angular/common/http';
import { HomeComponent } from './components/home/home.component';
import { TagCreateComponent } from './components/tag-create/tag-create.component';
import { FileDetailComponent } from './components/file-detail/file-detail.component';
import { UploadFileComponent } from './components/upload-file/upload-file.component';
import { EditFileComponent } from './components/edit-file/edit-file.component';

@NgModule({
  declarations: [
    AppComponent,
    FilesComponent,
    TagsComponent,
    SignInComponent,
    SignUpComponent,
    HomeComponent,
    TagCreateComponent,
    FileDetailComponent,
    UploadFileComponent,
    EditFileComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [UserService],
  bootstrap: [AppComponent]
})
export class AppModule { }
