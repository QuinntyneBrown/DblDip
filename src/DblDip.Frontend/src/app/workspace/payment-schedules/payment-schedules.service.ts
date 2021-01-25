import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { PaymentSchedule } from './payment-schedule';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PaymentSchedulesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<PaymentSchedule[]> {
    return this._client.get<{ paymentSchedules: PaymentSchedule[] }>(`${this._baseUrl}api/paymentSchedules`)
      .pipe(
        map(x => x.paymentSchedules)
      );
  }

  public getById(options: { paymentScheduleId: number }): Observable<PaymentSchedule> {
    return this._client.get<{ paymentSchedule: PaymentSchedule }>(`${this._baseUrl}api/paymentSchedules/${options.paymentScheduleId}`)
      .pipe(
        map(x => x.paymentSchedule)
      );
  }

  public remove(options: { paymentSchedule: PaymentSchedule }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/paymentSchedules/${options.paymentSchedule.paymentScheduleId}`);
  }

  public save(options: { paymentSchedule: PaymentSchedule }): Observable<{ paymentScheduleId: number }> {
    return this._client.post<{ paymentScheduleId: number }>(`${this._baseUrl}api/paymentSchedules`, { paymentSchedule: options.paymentSchedule });
  }  
}
