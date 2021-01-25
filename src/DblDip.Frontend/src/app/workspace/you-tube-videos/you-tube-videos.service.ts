import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { YouTubeVideo } from './you-tube-video';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class YouTubeVideosService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<YouTubeVideo[]> {
    return this._client.get<{ youTubeVideos: YouTubeVideo[] }>(`${this._baseUrl}api/youTubeVideos`)
      .pipe(
        map(x => x.youTubeVideos)
      );
  }

  public getById(options: { youTubeVideoId: number }): Observable<YouTubeVideo> {
    return this._client.get<{ youTubeVideo: YouTubeVideo }>(`${this._baseUrl}api/youTubeVideos/${options.youTubeVideoId}`)
      .pipe(
        map(x => x.youTubeVideo)
      );
  }

  public remove(options: { youTubeVideo: YouTubeVideo }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/youTubeVideos/${options.youTubeVideo.youTubeVideoId}`);
  }

  public save(options: { youTubeVideo: YouTubeVideo }): Observable<{ youTubeVideoId: number }> {
    return this._client.post<{ youTubeVideoId: number }>(`${this._baseUrl}api/youTubeVideos`, { youTubeVideo: options.youTubeVideo });
  }  
}
