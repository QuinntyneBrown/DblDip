import { ChangeDetectionStrategy, Component, OnDestroy, OnInit } from '@angular/core';
import { MatTableDataSource } from '@angular/material/table';
import { replace } from '@core/replace';
import { DialogService } from '@shared/dialog.service';
import { combineLatest, Observable, of, Subject } from 'rxjs';
import { map, takeUntil, tap } from 'rxjs/operators';
import { Rate } from '../rate';
import { RateDetailComponent } from '../rate-detail/rate-detail.component';
import { RateService } from '../rate.service';
import { pluckOut } from '@core/pluck-out';
import { ComponentStore } from '@ngrx/component-store';
import { Router } from '@angular/router';

@Component({
  selector: 'app-rate-list',
  templateUrl: './rate-list.component.html',
  styleUrls: ['./rate-list.component.scss'],
  providers: [
    ComponentStore
  ],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RateListComponent implements OnDestroy {

  private readonly _destroyed: Subject<void> = new Subject();
  
  private readonly createRate = this._componentStore.updater((state: { rates: Rate[] },rate: Rate) => {    
    state.rates.push(rate);
    return {
      rates: state.rates
    }
  });

  private readonly updateRate = this._componentStore.updater((state: { rates: Rate[] },rate: Rate) => {    
    return {
      rates: replace({ items: state.rates, value: rate, key: "rateId" })
    }
  });

  private readonly deleteRate = this._componentStore.updater((state: { rates: Rate[] },rate: Rate) => {    
    return {
      rates: pluckOut({ items: state.rates, value: rate, key: "rateId" })
    }
  });

  public readonly vm$: Observable<{
    dataSource$: any,
    columnsToDisplay: string[]
  }> = combineLatest([
    this._rateService.get(),
    of(["name","actions"])    
  ])
  .pipe(
    map(([rates, columnsToDisplay]) => {

      this._componentStore.setState({ rates });

      const dataSource$ = this._componentStore
      .select( state => ({ rates: state.rates }))
      .pipe(
        map(x => new MatTableDataSource(x.rates))
        );

      return {
        dataSource$,
        columnsToDisplay
      }
    })
  );

  constructor(
    private readonly _rateService: RateService,
    private readonly _dialogService: DialogService,
    private readonly _componentStore: ComponentStore<{ rates: Rate[] }>
  ) { }

  public edit(rate: Rate) {
    const component = this._dialogService.open<RateDetailComponent>(RateDetailComponent);
    component.rate$.next(rate);    
    component.saved
    .pipe(
      takeUntil(this._destroyed),
      tap(x => this.updateRate(x))
    ).subscribe();
  }

  public create() {
    this._dialogService.open<RateDetailComponent>(RateDetailComponent)
    .saved
    .pipe(
      takeUntil(this._destroyed),
      tap(x => this.createRate(x))
    ).subscribe();
  }

  public delete(rate: Rate) {    
    this.deleteRate(rate);
    this._rateService.remove({ rate }).pipe(
      takeUntil(this._destroyed) 
    ).subscribe();
  }
  
  ngOnDestroy() {
    this._destroyed.next();
    this._destroyed.complete();
  }
}
