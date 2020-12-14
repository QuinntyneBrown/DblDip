import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PublicRoutingModule } from './public-routing.module';
import { PublicComponent } from './public/public.component';
import { PublicHeaderComponent } from './public-header/public-header.component';
import { SharedModule } from '../shared/shared.module';
import { TestimonialsModule } from './testimonials/testimonials.module';
import { AboutModule } from './about/about.module';
import { PricingModule } from './pricing/pricing.module';
import { ContactModule } from './contact/contact.module';



@NgModule({
  declarations: [
    PublicComponent, 
    PublicHeaderComponent,
  ],
  imports: [
    AboutModule,
    ContactModule,
    PricingModule,
    TestimonialsModule,
    CommonModule,
    SharedModule,
    PublicRoutingModule,
  ]
})
export class PublicModule { }
