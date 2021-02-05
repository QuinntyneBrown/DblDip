import { ChangeDetectionStrategy, Component, OnDestroy, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { replace } from '@core/replace';
import { DialogService } from '@shared/dialog.service';
import { combineLatest, Observable, of, Subject } from 'rxjs';
import { map, takeUntil, tap } from 'rxjs/operators';
import { Account } from '../account';
import { AccountDetailComponent } from '../account-detail/account-detail.component';
import { AccountService } from '../account.service';
import { pluckOut } from '@core/pluck-out';
import { ComponentStore } from '@ngrx/component-store';
import { Router } from '@angular/router';

@Component({
  selector: 'app-account-list',
  templateUrl: './account-list.component.html',
  styleUrls: ['./account-list.component.scss'],
  providers: [
    ComponentStore
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AccountListComponent implements OnDestroy {

  private readonly _destroyed: Subject<void> = new Subject();
  
  private readonly createAccount = this._componentStore.updater((state: { accounts: Account[] },account: Account) => {    
    state.accounts.push(account);
    return {
      accounts: state.accounts
    }
  });

  private readonly updateAccount = this._componentStore.updater((state: { accounts: Account[] },account: Account) => {    
    return {
      accounts: replace({ items: state.accounts, value: account, key: "accountId" })
    }
  });

  private readonly deleteAccount = this._componentStore.updater((state: { accounts: Account[] },account: Account) => {    
    return {
      accounts: pluckOut({ items: state.accounts, value: account, key: "accountId" })
    }
  });

  public readonly vm$: Observable<{
    dataSource$: Observable<MatTableDataSource<Account>>,
    columnsToDisplay: string[]
  }> = combineLatest([
    this._accountService.get(),
    of(["name","actions"])    
  ])
  .pipe(
    map(([accounts, columnsToDisplay]) => {

      this._componentStore.setState({ accounts });

      return {
        dataSource$: this._componentStore.select((state) => ({
          accounts: state.accounts,
        })).pipe(
          map(x => new MatTableDataSource(x.accounts))),
        columnsToDisplay
      }
    })
  );

  constructor(
    private readonly _accountService: AccountService,
    private readonly _dialogService: DialogService,
    private readonly _componentStore: ComponentStore<{ accounts: Account[] }>,
    private readonly _router: Router
  ) { }

  public edit(account: Account) {
    const component = this._dialogService.open<AccountDetailComponent>(AccountDetailComponent);
    component.account$.next(account);    
    component.saved
    .pipe(
      takeUntil(this._destroyed),
      tap(x => this.updateAccount(x))
    ).subscribe();
  }

  public create() {
    this._dialogService.open<AccountDetailComponent>(AccountDetailComponent)
    .saved
    .pipe(
      takeUntil(this._destroyed),
      tap(x => this.createAccount(x))
    ).subscribe();
  }

  public delete(account: Account) {    
    this.deleteAccount(account);
    this._accountService.remove({ account }).pipe(
      takeUntil(this._destroyed) 
    ).subscribe();
  }
  
  public view(account: Account) {    
    this._router.navigateByUrl(`/accounts/${account.accountId}`);
  }
  
  ngOnDestroy() {
    this._destroyed.next();
    this._destroyed.complete();
  }
}
