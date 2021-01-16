import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManageDashboardsComponent } from './manage-dashboards.component';

describe('ManageDashboardsComponent', () => {
  let component: ManageDashboardsComponent;
  let fixture: ComponentFixture<ManageDashboardsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManageDashboardsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManageDashboardsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
