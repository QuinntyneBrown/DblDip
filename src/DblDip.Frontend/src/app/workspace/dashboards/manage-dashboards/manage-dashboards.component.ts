import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Dashboard } from '../dashboard';

@Component({
  selector: 'app-manage-dashboards',
  templateUrl: './manage-dashboards.component.html',
  styleUrls: ['./manage-dashboards.component.scss']
})
export class ManageDashboardsComponent implements OnInit {

  public dashboards$: Observable<Dashboard[]> | null = null;

  constructor() { }

  ngOnInit(): void {
  }

}
