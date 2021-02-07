import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Account } from './account';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Account[]> {
    return this._client.get<{ accounts: Account[] }>(`${this._baseUrl}api/accounts`)
      .pipe(
        map(x => x.accounts)
      );
  }

  public getById(options: { accountId: string }): Observable<Account> {
    return this._client.get<{ account: Account }>(`${this._baseUrl}api/accounts/${options.accountId}`)
      .pipe(
        map(x => x.account)
      );
  }

  public remove(options: { account: Account }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/accounts/${options.account.accountId}`);
  }

  public create(options: { account: Account }): Observable<{ account: Account }> {
    return this._client.post<{ account: Account }>(`${this._baseUrl}api/accounts`, { account: options.account });
  }
  
  public update(options: { account: Account }): Observable<{ account: Account }> {
    return this._client.put<{ account: Account }>(`${this._baseUrl}api/accounts`, { account: options.account });
  }

  public setCurrentProfile(options: { profileId: string }): Observable<{ accessToken: string }> {
    return this._client.put<{ accessToken: string }>(`${this._baseUrl}api/accounts`, { profileId: options.profileId });
  }  
}
