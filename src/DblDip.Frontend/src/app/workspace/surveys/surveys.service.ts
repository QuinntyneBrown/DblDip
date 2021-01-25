import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Survey } from './survey';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class SurveysService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Survey[]> {
    return this._client.get<{ surveys: Survey[] }>(`${this._baseUrl}api/surveys`)
      .pipe(
        map(x => x.surveys)
      );
  }

  public getById(options: { surveyId: number }): Observable<Survey> {
    return this._client.get<{ survey: Survey }>(`${this._baseUrl}api/surveys/${options.surveyId}`)
      .pipe(
        map(x => x.survey)
      );
  }

  public remove(options: { survey: Survey }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/surveys/${options.survey.surveyId}`);
  }

  public save(options: { survey: Survey }): Observable<{ surveyId: number }> {
    return this._client.post<{ surveyId: number }>(`${this._baseUrl}api/surveys`, { survey: options.survey });
  }  
}
