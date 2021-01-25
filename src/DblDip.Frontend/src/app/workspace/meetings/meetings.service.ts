import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Meeting } from './meeting';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MeetingsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Meeting[]> {
    return this._client.get<{ meetings: Meeting[] }>(`${this._baseUrl}api/meetings`)
      .pipe(
        map(x => x.meetings)
      );
  }

  public getById(options: { meetingId: number }): Observable<Meeting> {
    return this._client.get<{ meeting: Meeting }>(`${this._baseUrl}api/meetings/${options.meetingId}`)
      .pipe(
        map(x => x.meeting)
      );
  }

  public remove(options: { meeting: Meeting }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/meetings/${options.meeting.meetingId}`);
  }

  public save(options: { meeting: Meeting }): Observable<{ meetingId: number }> {
    return this._client.post<{ meetingId: number }>(`${this._baseUrl}api/meetings`, { meeting: options.meeting });
  }  
}
