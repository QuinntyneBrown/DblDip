import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Library } from './library';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class LibrariesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Library[]> {
    return this._client.get<{ libraries: Library[] }>(`${this._baseUrl}api/libraries`)
      .pipe(
        map(x => x.libraries)
      );
  }

  public getById(options: { libraryId: number }): Observable<Library> {
    return this._client.get<{ library: Library }>(`${this._baseUrl}api/libraries/${options.libraryId}`)
      .pipe(
        map(x => x.library)
      );
  }

  public remove(options: { library: Library }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/libraries/${options.library.libraryId}`);
  }

  public save(options: { library: Library }): Observable<{ libraryId: number }> {
    return this._client.post<{ libraryId: number }>(`${this._baseUrl}api/libraries`, { library: options.library });
  }  
}
