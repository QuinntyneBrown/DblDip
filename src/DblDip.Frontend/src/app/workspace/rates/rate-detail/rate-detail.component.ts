import { OverlayRef } from '@angular/cdk/overlay';
import { Component, EventEmitter, OnDestroy, Output } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { BehaviorSubject, combineLatest, Observable, Subject } from 'rxjs';
import { map, takeUntil, tap } from 'rxjs/operators';
import { Rate } from '../rate';
import { RateService } from '../rate.service';

@Component({
  selector: 'app-rate-detail',
  templateUrl: './rate-detail.component.html',
  styleUrls: ['./rate-detail.component.scss']
})
export class RateDetailComponent implements OnDestroy {

  private readonly _destroyed: Subject<void> = new Subject();

  public rate$: BehaviorSubject<Rate> = new BehaviorSubject(null as any);

  @Output() public saved = new EventEmitter();

  public vm$ = combineLatest([
    this.rate$
  ]).pipe(
    map(([rate]) => {
      return {
        form: new FormGroup({
          rate: new FormControl(rate, [])
        })
      }
    })
  )

  constructor(
    private readonly _overlayRef: OverlayRef,
    private readonly _rateService: RateService) {     
  }

  public save(vm: { form: FormGroup}) {
    const rate = vm.form.value.rate;
    let obs$: Observable<{ rate: Rate }>;
    if(rate.rateId) {
      obs$ = this._rateService.update({ rate })
    }   
    else {
      obs$ = this._rateService.create({ rate })
    } 

    obs$.pipe(
      takeUntil(this._destroyed),      
      tap(x => {
        this.saved.next(x.rate);
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
