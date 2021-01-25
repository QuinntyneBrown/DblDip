import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProfileEditorComponent } from './profile-editor/profile-editor.component';
import { SharedModule } from '@shared';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SelectProfileComponent } from './select-profile/select-profile.component';

@NgModule({
  declarations: [
    ProfileEditorComponent,
    SelectProfileComponent,
  ],
  imports: [
    SharedModule,
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    CommonModule
  ]
})
export class ProfilesModule { }
