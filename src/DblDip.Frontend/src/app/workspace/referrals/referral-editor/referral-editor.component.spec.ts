import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReferralEditorComponent } from './referral-editor.component';

describe('ReferralEditorComponent', () => {
  let component: ReferralEditorComponent;
  let fixture: ComponentFixture<ReferralEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReferralEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReferralEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
