import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RateEditorComponent } from './rate-editor.component';

describe('RateEditorComponent', () => {
  let component: RateEditorComponent;
  let fixture: ComponentFixture<RateEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RateEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RateEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
