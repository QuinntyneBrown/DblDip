import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Post } from './post';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PostsService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Post[]> {
    return this._client.get<{ posts: Post[] }>(`${this._baseUrl}api/posts`)
      .pipe(
        map(x => x.posts)
      );
  }

  public getById(options: { postId: number }): Observable<Post> {
    return this._client.get<{ post: Post }>(`${this._baseUrl}api/posts/${options.postId}`)
      .pipe(
        map(x => x.post)
      );
  }

  public remove(options: { post: Post }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/posts/${options.post.postId}`);
  }

  public create(options: { post: Post }): Observable<{ postId: number }> {
    return this._client.post<{ postId: number }>(`${this._baseUrl}api/posts`, { post: options.post });
  }  
}
