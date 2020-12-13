import { Component, OnInit } from '@angular/core';
import { TestimonialsService } from 'src/app/workspace/testimonials/testimonials.service';

@Component({
  selector: 'app-testimonials',
  templateUrl: './testimonials.component.html',
  styleUrls: ['./testimonials.component.scss']
})
export class TestimonialsComponent implements OnInit {

  constructor(public testimonialsService: TestimonialsService) { }

  ngOnInit(): void {
  }

}
