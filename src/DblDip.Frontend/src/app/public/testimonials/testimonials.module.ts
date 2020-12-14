import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TestimonialsComponent } from './testimonials/testimonials.component';



@NgModule({
  declarations: [TestimonialsComponent],
  exports:[TestimonialsComponent],
  imports: [
    CommonModule
  ]
})
export class TestimonialsModule { }
