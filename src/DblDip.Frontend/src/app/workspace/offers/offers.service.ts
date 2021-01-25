import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Offer } from './offer';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class OffersService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Offer[]> {
    return this._client.get<{ offers: Offer[] }>(`${this._baseUrl}api/offers`)
      .pipe(
        map(x => x.offers)
      );
  }

  public getById(options: { offerId: number }): Observable<Offer> {
    return this._client.get<{ offer: Offer }>(`${this._baseUrl}api/offers/${options.offerId}`)
      .pipe(
        map(x => x.offer)
      );
  }

  public remove(options: { offer: Offer }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/offers/${options.offer.offerId}`);
  }

  public save(options: { offer: Offer }): Observable<{ offerId: number }> {
    return this._client.post<{ offerId: number }>(`${this._baseUrl}api/offers`, { offer: options.offer });
  }  
}
