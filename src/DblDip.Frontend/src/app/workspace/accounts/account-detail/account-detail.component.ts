import { OverlayRef } from '@angular/cdk/overlay';
import { Component, EventEmitter, OnDestroy, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BehaviorSubject, combineLatest, Observable, Subject } from 'rxjs';
import { map, takeUntil, tap } from 'rxjs/operators';
import { Account } from '../account';
import { AccountService } from '../account.service';

@Component({
  selector: 'app-account-detail',
  templateUrl: './account-detail.component.html',
  styleUrls: ['./account-detail.component.scss']
})
export class AccountDetailComponent implements OnDestroy {

  private readonly _destroyed: Subject<void> = new Subject();

  public account$: BehaviorSubject<Account> = new BehaviorSubject(null as any);

  @Output() public saved = new EventEmitter();

  public vm$ = combineLatest([
    this.account$
  ]).pipe(
    map(([account]) => {
      return {
        form: new FormGroup({
          account: new FormControl(account, [])
        })
      }
    })
  )

  constructor(
    private readonly _overlayRef: OverlayRef,
    private readonly _accountService: AccountService) {     
  }

  public save(vm: { form: FormGroup}) {
    const account = vm.form.value.account;
    let obs$: Observable<{ account: Account }>;
    if(account.accountId) {
      obs$ = this._accountService.update({ account })
    }   
    else {
      obs$ = this._accountService.create({ account })
    } 

    obs$.pipe(
      takeUntil(this._destroyed),      
      tap(x => {
        this.saved.next(x.account);
        this._overlayRef.dispose();
      })
    ).subscribe();
  }

  public cancel() {
    this._overlayRef.dispose();
  }

  ngOnDestroy() {
    this._destroyed.complete();
    this._destroyed.next();
  }
}
