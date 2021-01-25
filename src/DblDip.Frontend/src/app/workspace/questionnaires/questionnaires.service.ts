import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Questionnaire } from './questionnaire';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class QuestionnairesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Questionnaire[]> {
    return this._client.get<{ questionnaires: Questionnaire[] }>(`${this._baseUrl}api/questionnaires`)
      .pipe(
        map(x => x.questionnaires)
      );
  }

  public getById(options: { questionnaireId: number }): Observable<Questionnaire> {
    return this._client.get<{ questionnaire: Questionnaire }>(`${this._baseUrl}api/questionnaires/${options.questionnaireId}`)
      .pipe(
        map(x => x.questionnaire)
      );
  }

  public remove(options: { questionnaire: Questionnaire }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/questionnaires/${options.questionnaire.questionnaireId}`);
  }

  public save(options: { questionnaire: Questionnaire }): Observable<{ questionnaireId: number }> {
    return this._client.post<{ questionnaireId: number }>(`${this._baseUrl}api/questionnaires`, { questionnaire: options.questionnaire });
  }  
}
