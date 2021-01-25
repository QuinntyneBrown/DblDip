import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Participant } from './participant';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ParticipantsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Participant[]> {
    return this._client.get<{ participants: Participant[] }>(`${this._baseUrl}api/participants`)
      .pipe(
        map(x => x.participants)
      );
  }

  public getById(options: { participantId: number }): Observable<Participant> {
    return this._client.get<{ participant: Participant }>(`${this._baseUrl}api/participants/${options.participantId}`)
      .pipe(
        map(x => x.participant)
      );
  }

  public remove(options: { participant: Participant }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/participants/${options.participant.participantId}`);
  }

  public create(options: { participant: Participant }): Observable<{ participantId: number }> {
    return this._client.post<{ participantId: number }>(`${this._baseUrl}api/participants`, { participant: options.participant });
  }  
}
