import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Referral } from './referral';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ReferralsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Referral[]> {
    return this._client.get<{ referrals: Referral[] }>(`${this._baseUrl}api/referrals`)
      .pipe(
        map(x => x.referrals)
      );
  }

  public getById(options: { referralId: number }): Observable<Referral> {
    return this._client.get<{ referral: Referral }>(`${this._baseUrl}api/referrals/${options.referralId}`)
      .pipe(
        map(x => x.referral)
      );
  }

  public remove(options: { referral: Referral }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/referrals/${options.referral.referralId}`);
  }

  public save(options: { referral: Referral }): Observable<{ referralId: number }> {
    return this._client.post<{ referralId: number }>(`${this._baseUrl}api/referrals`, { referral: options.referral });
  }  
}
