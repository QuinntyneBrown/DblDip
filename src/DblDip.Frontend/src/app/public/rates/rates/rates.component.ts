import { Component, OnInit } from '@angular/core';
import { QuotesService } from '../../../workspace/quotes/quotes.service';
@Component({
  selector: 'app-rates',
  templateUrl: './rates.component.html',
  styleUrls: ['./rates.component.scss']
})
export class RatesComponent implements OnInit {

  constructor(public quoteService: QuotesService) { }

  ngOnInit(): void {
  }

}
