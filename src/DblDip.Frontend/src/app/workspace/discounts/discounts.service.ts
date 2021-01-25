import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Discount } from './discount';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class DiscountsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Discount[]> {
    return this._client.get<{ discounts: Discount[] }>(`${this._baseUrl}api/discounts`)
      .pipe(
        map(x => x.discounts)
      );
  }

  public getById(options: { discountId: number }): Observable<Discount> {
    return this._client.get<{ discount: Discount }>(`${this._baseUrl}api/discounts/${options.discountId}`)
      .pipe(
        map(x => x.discount)
      );
  }

  public remove(options: { discount: Discount }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/discounts/${options.discount.discountId}`);
  }

  public save(options: { discount: Discount }): Observable<{ discountId: number }> {
    return this._client.post<{ discountId: number }>(`${this._baseUrl}api/discounts`, { discount: options.discount });
  }  
}
