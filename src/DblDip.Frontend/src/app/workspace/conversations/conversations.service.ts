import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Conversation } from './conversation';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ConversationsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Conversation[]> {
    return this._client.get<{ conversations: Conversation[] }>(`${this._baseUrl}api/conversations`)
      .pipe(
        map(x => x.conversations)
      );
  }

  public getById(options: { conversationId: number }): Observable<Conversation> {
    return this._client.get<{ conversation: Conversation }>(`${this._baseUrl}api/conversations/${options.conversationId}`)
      .pipe(
        map(x => x.conversation)
      );
  }

  public remove(options: { conversation: Conversation }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/conversations/${options.conversation.conversationId}`);
  }

  public save(options: { conversation: Conversation }): Observable<{ conversationId: number }> {
    return this._client.post<{ conversationId: number }>(`${this._baseUrl}api/conversations`, { conversation: options.conversation });
  }  
}
