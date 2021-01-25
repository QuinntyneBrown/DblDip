import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MeetingEditorComponent } from './meeting-editor.component';

describe('MeetingEditorComponent', () => {
  let component: MeetingEditorComponent;
  let fixture: ComponentFixture<MeetingEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ MeetingEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(MeetingEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
