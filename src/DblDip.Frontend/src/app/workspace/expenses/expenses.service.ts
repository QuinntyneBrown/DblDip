import { Injectable, Inject } from '@angular/core';
import { baseUrl } from '@core/constants';
import { HttpClient } from '@angular/common/http';
import { Expense } from './expense';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ExpensesService {

  constructor(
    @Inject(baseUrl) private _baseUrl: string,
    private _client: HttpClient
  ) { }

  public get(): Observable<Expense[]> {
    return this._client.get<{ expenses: Expense[] }>(`${this._baseUrl}api/expenses`)
      .pipe(
        map(x => x.expenses)
      );
  }

  public getById(options: { expenseId: number }): Observable<Expense> {
    return this._client.get<{ expense: Expense }>(`${this._baseUrl}api/expenses/${options.expenseId}`)
      .pipe(
        map(x => x.expense)
      );
  }

  public remove(options: { expense: Expense }): Observable<void> {
    return this._client.delete<void>(`${this._baseUrl}api/expenses/${options.expense.expenseId}`);
  }

  public save(options: { expense: Expense }): Observable<{ expenseId: number }> {
    return this._client.post<{ expenseId: number }>(`${this._baseUrl}api/expenses`, { expense: options.expense });
  }  
}
