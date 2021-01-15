import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Profile } from './profile';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ProfilesService {

  public currentProfile$: BehaviorSubject<Profile> = new BehaviorSubject(null as any);

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Profile[]> {
    return this._client.get<{ profiles: Profile[] }>(`${this._baseUrl}api/profiles`)
      .pipe(
        map(x => x.profiles)
      );
  }

  public getByCurrentAccount(): Observable<Profile[]> {
    return this._client.get<{ profiles: Profile[] }>(`${this._baseUrl}api/profiles/current/account`)
      .pipe(
        map(x => x.profiles)
      );
  }

  public getById(options: { profileId: number }): Observable<Profile> {
    return this._client.get<{ profile: Profile }>(`${this._baseUrl}api/profiles/${options.profileId}`)
      .pipe(
        map(x => x.profile)
      );
  }

  public remove(options: { profile: Profile }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/profiles/${options.profile.profileId}`);
  }

  public save(options: { profile: Profile }): Observable<{ profileId: number }> {
    return this._client.post<{ profileId: number }>(`${this._baseUrl}api/profiles`, { profile: options.profile });
  }  
}
