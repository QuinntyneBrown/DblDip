import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Dashboard } from '../dashboard';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements OnInit {

  public dashboard$: Observable<Dashboard> | null = null;
  
  constructor() { }

  ngOnInit(): void {
  }

}
