import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PointEditorComponent } from './point-editor.component';

describe('PointEditorComponent', () => {
  let component: PointEditorComponent;
  let fixture: ComponentFixture<PointEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PointEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PointEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
