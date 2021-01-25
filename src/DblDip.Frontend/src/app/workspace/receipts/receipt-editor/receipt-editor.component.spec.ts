import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ReceiptEditorComponent } from './receipt-editor.component';

describe('ReceiptEditorComponent', () => {
  let component: ReceiptEditorComponent;
  let fixture: ComponentFixture<ReceiptEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ReceiptEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ReceiptEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
