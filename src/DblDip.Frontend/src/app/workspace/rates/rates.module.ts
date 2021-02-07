import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RateEditorComponent } from './rate-editor/rate-editor.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RateListComponent } from './rate-list/rate-list.component';
import { RateDetailComponent } from './rate-detail/rate-detail.component';
import { SharedModule } from '@shared';



@NgModule({
  declarations: [RateEditorComponent, RateListComponent, RateDetailComponent],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    SharedModule,
    FormsModule
  ]
})
export class RatesModule { }
