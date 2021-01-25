import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Receipt } from './receipt';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReceiptsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Receipt[]> {
    return this._client.get<{ receipts: Receipt[] }>(`${this._baseUrl}api/receipts`)
      .pipe(
        map(x => x.receipts)
      );
  }

  public getById(options: { receiptId: number }): Observable<Receipt> {
    return this._client.get<{ receipt: Receipt }>(`${this._baseUrl}api/receipts/${options.receiptId}`)
      .pipe(
        map(x => x.receipt)
      );
  }

  public remove(options: { receipt: Receipt }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/receipts/${options.receipt.receiptId}`);
  }

  public save(options: { receipt: Receipt }): Observable<{ receiptId: number }> {
    return this._client.post<{ receiptId: number }>(`${this._baseUrl}api/receipts`, { receipt: options.receipt });
  }  
}
