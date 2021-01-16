import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Dashboard } from '../dashboard';

@Component({
  selector: 'app-dashboard-header',
  templateUrl: './dashboard-header.component.html',
  styleUrls: ['./dashboard-header.component.scss']
})
export class DashboardHeaderComponent implements OnInit {

  public dashboards$: Observable<Dashboard[]> | null = null;
  
  constructor() { }

  ngOnInit(): void {
  }

}
