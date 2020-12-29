import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Quote } from './quote';
import { Observable } from 'rxjs';
import { baseUrl } from '@core/constants';

@Injectable({
  providedIn: 'root'
})
export class QuotesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public createWeddingQuote(options: { quote: Quote }): Observable<{ id: number }> {
    return this._client.post<{ id: number }>(`${this._baseUrl}api/quotes`, { quote: options.quote });
  }  
}
