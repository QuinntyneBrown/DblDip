import { Component, OnInit } from '@angular/core';
import { QuotesService } from 'src/app/admin/quotes/quotes.service';

@Component({
  selector: 'app-quote',
  templateUrl: './quote.component.html',
  styleUrls: ['./quote.component.scss']
})
export class QuoteComponent implements OnInit {

  constructor(public quoteService: QuotesService) { }

  ngOnInit(): void {
  }

  public quote() {
    alert("?")
  }
}
