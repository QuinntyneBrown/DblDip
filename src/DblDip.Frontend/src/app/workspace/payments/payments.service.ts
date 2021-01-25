import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Payment } from './payment';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PaymentsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Payment[]> {
    return this._client.get<{ payments: Payment[] }>(`${this._baseUrl}api/payments`)
      .pipe(
        map(x => x.payments)
      );
  }

  public getById(options: { paymentId: number }): Observable<Payment> {
    return this._client.get<{ payment: Payment }>(`${this._baseUrl}api/payments/${options.paymentId}`)
      .pipe(
        map(x => x.payment)
      );
  }

  public remove(options: { payment: Payment }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/payments/${options.payment.paymentId}`);
  }

  public save(options: { payment: Payment }): Observable<{ paymentId: number }> {
    return this._client.post<{ paymentId: number }>(`${this._baseUrl}api/payments`, { payment: options.payment });
  }  
}
