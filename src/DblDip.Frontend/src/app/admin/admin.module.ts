import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { AdminRoutingModule } from './admin-routing.module';
import { SharedModule } from '../shared/shared.module';
import { AdminComponent } from './admin.component';

@NgModule({
  declarations: [
    AdminComponent
  ],
  providers: [

  ],
  imports: [
    CommonModule,
    AdminRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class AdminModule { }
