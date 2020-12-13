import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicRoutingModule } from './public-routing.module';
import { HomeComponent } from './home/home.component';
import { PublicComponent } from './public/public.component';



@NgModule({
  declarations: [HomeComponent, PublicComponent],
  imports: [
    CommonModule,
    PublicRoutingModule,
  ]
})
export class PublicModule { }
