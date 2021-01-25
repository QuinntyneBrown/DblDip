import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { ShotList } from './shot-list';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ShotListsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<ShotList[]> {
    return this._client.get<{ shotLists: ShotList[] }>(`${this._baseUrl}api/shotLists`)
      .pipe(
        map(x => x.shotLists)
      );
  }

  public getById(options: { shotListId: number }): Observable<ShotList> {
    return this._client.get<{ shotList: ShotList }>(`${this._baseUrl}api/shotLists/${options.shotListId}`)
      .pipe(
        map(x => x.shotList)
      );
  }

  public remove(options: { shotList: ShotList }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/shotLists/${options.shotList.shotListId}`);
  }

  public save(options: { shotList: ShotList }): Observable<{ shotListId: number }> {
    return this._client.post<{ shotListId: number }>(`${this._baseUrl}api/shotLists`, { shotList: options.shotList });
  }  
}
