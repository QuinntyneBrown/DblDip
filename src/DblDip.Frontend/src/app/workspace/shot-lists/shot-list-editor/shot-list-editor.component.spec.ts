import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShotListEditorComponent } from './shot-list-editor.component';

describe('ShotListEditorComponent', () => {
  let component: ShotListEditorComponent;
  let fixture: ComponentFixture<ShotListEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShotListEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShotListEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
