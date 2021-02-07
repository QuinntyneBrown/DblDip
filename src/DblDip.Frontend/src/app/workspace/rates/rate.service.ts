import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Rate } from './rate';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RateService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Rate[]> {
    return this._client.get<{ rates: Rate[] }>(`${this._baseUrl}api/rates`)
      .pipe(
        map(x => x.rates)
      );
  }

  public getById(options: { rateId: string }): Observable<Rate> {
    return this._client.get<{ rate: Rate }>(`${this._baseUrl}api/rates/${options.rateId}`)
      .pipe(
        map(x => x.rate)
      );
  }

  public remove(options: { rate: Rate }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/rates/${options.rate.rateId}`);
  }

  public create(options: { rate: Rate }): Observable<{ rate: Rate }> {
    return this._client.post<{ rate: Rate }>(`${this._baseUrl}api/rates`, { rate: options.rate });
  }
  
  public update(options: { rate: Rate }): Observable<{ rate: Rate }> {
    return this._client.put<{ rate: Rate }>(`${this._baseUrl}api/rates`, { rate: options.rate });
  }
}
