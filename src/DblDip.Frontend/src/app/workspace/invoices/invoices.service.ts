import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Invoice } from './invoice';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class InvoicesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Invoice[]> {
    return this._client.get<{ invoices: Invoice[] }>(`${this._baseUrl}api/invoices`)
      .pipe(
        map(x => x.invoices)
      );
  }

  public getById(options: { invoiceId: number }): Observable<Invoice> {
    return this._client.get<{ invoice: Invoice }>(`${this._baseUrl}api/invoices/${options.invoiceId}`)
      .pipe(
        map(x => x.invoice)
      );
  }

  public remove(options: { invoice: Invoice }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/invoices/${options.invoice.invoiceId}`);
  }

  public save(options: { invoice: Invoice }): Observable<{ invoiceId: number }> {
    return this._client.post<{ invoiceId: number }>(`${this._baseUrl}api/invoices`, { invoice: options.invoice });
  }  
}
