import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SystemLocationEditorComponent } from './system-location-editor.component';

describe('SystemLocationEditorComponent', () => {
  let component: SystemLocationEditorComponent;
  let fixture: ComponentFixture<SystemLocationEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SystemLocationEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SystemLocationEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
