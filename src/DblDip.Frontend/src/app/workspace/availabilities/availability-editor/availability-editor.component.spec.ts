import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AvailabilityEditorComponent } from './availability-editor.component';

describe('AvailabilityEditorComponent', () => {
  let component: AvailabilityEditorComponent;
  let fixture: ComponentFixture<AvailabilityEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AvailabilityEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AvailabilityEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
