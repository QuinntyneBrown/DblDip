import { Injectable, Inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Quote } from './quote';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { baseUrl } from 'src/app/core/constants';

@Injectable({
  providedIn: 'root'
})
export class QuotesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Quote[]> {
    return this._client.get<{ quotes: Quote[] }>(`${this._baseUrl}api/quotes`)
      .pipe(
        map(x => x.quotes)
      );
  }

  public getById(options: { id: number }): Observable<Quote> {
    return this._client.get<{ quote: Quote }>(`${this._baseUrl}api/quotes/${options.id}`)
      .pipe(
        map(x => x.quote)
      );
  }

  public remove(options: { quote: Quote }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/quotes/${options.quote.id}`);
  }

  public save(options: { quote: Quote }): Observable<{ id: number }> {
    return this._client.post<{ id: number }>(`${this._baseUrl}api/quotes`, { quote: options.quote });
  }  
}
