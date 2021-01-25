import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Board } from './board';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class BoardsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Board[]> {
    return this._client.get<{ boards: Board[] }>(`${this._baseUrl}api/boards`)
      .pipe(
        map(x => x.boards)
      );
  }

  public getById(options: { boardId: number }): Observable<Board> {
    return this._client.get<{ board: Board }>(`${this._baseUrl}api/boards/${options.boardId}`)
      .pipe(
        map(x => x.board)
      );
  }

  public remove(options: { board: Board }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/boards/${options.board.boardId}`);
  }

  public save(options: { board: Board }): Observable<{ boardId: number }> {
    return this._client.post<{ boardId: number }>(`${this._baseUrl}api/boards`, { board: options.board });
  }  
}
