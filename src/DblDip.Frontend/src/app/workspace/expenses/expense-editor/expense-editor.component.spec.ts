import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ExpenseEditorComponent } from './expense-editor.component';

describe('ExpenseEditorComponent', () => {
  let component: ExpenseEditorComponent;
  let fixture: ComponentFixture<ExpenseEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ExpenseEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ExpenseEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
