import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Observable, Subject } from 'rxjs';
import { Dashboard } from '../dashboard';

@Component({
  selector: 'app-dashboard-card-configuration',
  templateUrl: './dashboard-card-configuration.component.html',
  styleUrls: ['./dashboard-card-configuration.component.scss']
})
export class DashboardCardConfigurationComponent implements OnInit, OnDestroy{

  public readonly destroyed$: Subject<void> = new Subject();

  public dashboard$: Observable<Dashboard> | null = null;
  
  public form: FormGroup = new FormGroup({
    top: new FormControl(null, []),
    left: new FormControl(null, []),
    height: new FormControl(null, []),
    width: new FormControl(null, [])
  });
  
  constructor() { }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.destroyed$.next();
    this.destroyed$.complete();
  }

}
