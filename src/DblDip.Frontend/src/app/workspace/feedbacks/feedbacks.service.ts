import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Feedback } from './feedback';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class FeedbacksService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Feedback[]> {
    return this._client.get<{ feedbacks: Feedback[] }>(`${this._baseUrl}api/feedbacks`)
      .pipe(
        map(x => x.feedbacks)
      );
  }

  public getById(options: { feedbackId: number }): Observable<Feedback> {
    return this._client.get<{ feedback: Feedback }>(`${this._baseUrl}api/feedbacks/${options.feedbackId}`)
      .pipe(
        map(x => x.feedback)
      );
  }

  public remove(options: { feedback: Feedback }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/feedbacks/${options.feedback.feedbackId}`);
  }

  public save(options: { feedback: Feedback }): Observable<{ feedbackId: number }> {
    return this._client.post<{ feedbackId: number }>(`${this._baseUrl}api/feedbacks`, { feedback: options.feedback });
  }  
}
