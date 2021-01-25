import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Story } from './story';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StoriesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Story[]> {
    return this._client.get<{ stories: Story[] }>(`${this._baseUrl}api/stories`)
      .pipe(
        map(x => x.stories)
      );
  }

  public getById(options: { storyId: number }): Observable<Story> {
    return this._client.get<{ story: Story }>(`${this._baseUrl}api/stories/${options.storyId}`)
      .pipe(
        map(x => x.story)
      );
  }

  public remove(options: { story: Story }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/stories/${options.story.storyId}`);
  }

  public save(options: { story: Story }): Observable<{ storyId: number }> {
    return this._client.post<{ storyId: number }>(`${this._baseUrl}api/stories`, { story: options.story });
  }  
}
