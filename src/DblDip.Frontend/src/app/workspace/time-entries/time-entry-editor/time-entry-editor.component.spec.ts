import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TimeEntryEditorComponent } from './time-entry-editor.component';

describe('TimeEntryEditorComponent', () => {
  let component: TimeEntryEditorComponent;
  let fixture: ComponentFixture<TimeEntryEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ TimeEntryEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(TimeEntryEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
