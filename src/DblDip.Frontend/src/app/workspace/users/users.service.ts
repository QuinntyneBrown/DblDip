import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { User } from './user';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class UsersService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<User[]> {
    return this._client.get<{ users: User[] }>(`${this._baseUrl}api/users`)
      .pipe(
        map(x => x.users)
      );
  }

  public getById(options: { userId: number }): Observable<User> {
    return this._client.get<{ user: User }>(`${this._baseUrl}api/users/${options.userId}`)
      .pipe(
        map(x => x.user)
      );
  }

  public remove(options: { user: User }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/users/${options.user.userId}`);
  }

  public save(options: { user: User }): Observable<{ userId: number }> {
    return this._client.post<{ userId: number }>(`${this._baseUrl}api/users`, { user: options.user });
  }  
}
