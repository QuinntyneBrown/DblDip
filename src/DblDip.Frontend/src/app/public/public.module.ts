import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SharedModule } from '../shared/shared.module';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { PublicRoutingModule } from './public-routing.module';
import { HomeComponent } from './home/home.component';
import { PublicComponent } from './public.component';

@NgModule({
  declarations: [
    HomeComponent,
    PublicComponent
  ],
  providers: [

  ],
  imports: [
    CommonModule,
    PublicRoutingModule,
    SharedModule,
    ReactiveFormsModule,
    FormsModule
  ]
})
export class PublicModule { }
