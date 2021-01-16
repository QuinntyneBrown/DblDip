import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardCardConfigurationComponent } from './dashboard-card-configuration.component';

describe('DashboardCardConfigurationComponent', () => {
  let component: DashboardCardConfigurationComponent;
  let fixture: ComponentFixture<DashboardCardConfigurationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DashboardCardConfigurationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardCardConfigurationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
