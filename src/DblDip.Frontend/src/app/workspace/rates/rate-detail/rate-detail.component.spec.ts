import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RateDetailComponent } from './rate-detail.component';

describe('RateDetailComponent', () => {
  let component: RateDetailComponent;
  let fixture: ComponentFixture<RateDetailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RateDetailComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RateDetailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
