import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaymentScheduleEditorComponent } from './payment-schedule-editor.component';

describe('PaymentScheduleEditorComponent', () => {
  let component: PaymentScheduleEditorComponent;
  let fixture: ComponentFixture<PaymentScheduleEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaymentScheduleEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaymentScheduleEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
