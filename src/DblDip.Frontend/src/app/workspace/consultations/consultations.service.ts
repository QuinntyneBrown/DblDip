import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Consultation } from './consultation';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ConsultationsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Consultation[]> {
    return this._client.get<{ consultations: Consultation[] }>(`${this._baseUrl}api/consultations`)
      .pipe(
        map(x => x.consultations)
      );
  }

  public getById(options: { consultationId: number }): Observable<Consultation> {
    return this._client.get<{ consultation: Consultation }>(`${this._baseUrl}api/consultations/${options.consultationId}`)
      .pipe(
        map(x => x.consultation)
      );
  }

  public remove(options: { consultation: Consultation }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/consultations/${options.consultation.consultationId}`);
  }

  public save(options: { consultation: Consultation }): Observable<{ consultationId: number }> {
    return this._client.post<{ consultationId: number }>(`${this._baseUrl}api/consultations`, { consultation: options.consultation });
  }  
}
