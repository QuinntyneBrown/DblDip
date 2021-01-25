import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Task } from './task';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Task[]> {
    return this._client.get<{ tasks: Task[] }>(`${this._baseUrl}api/tasks`)
      .pipe(
        map(x => x.tasks)
      );
  }

  public getById(options: { taskId: number }): Observable<Task> {
    return this._client.get<{ task: Task }>(`${this._baseUrl}api/tasks/${options.taskId}`)
      .pipe(
        map(x => x.task)
      );
  }

  public remove(options: { task: Task }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/tasks/${options.task.taskId}`);
  }

  public save(options: { task: Task }): Observable<{ taskId: number }> {
    return this._client.post<{ taskId: number }>(`${this._baseUrl}api/tasks`, { task: options.task });
  }  
}
