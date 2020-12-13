import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicRoutingModule } from './public-routing.module';
import { HomeComponent } from './home/home.component';
import { PublicComponent } from './public/public.component';
import { PublicHeaderComponent } from './public-header/public-header.component';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [HomeComponent, PublicComponent, PublicHeaderComponent],
  imports: [
    CommonModule,
    SharedModule,
    PublicRoutingModule,
  ]
})
export class PublicModule { }
