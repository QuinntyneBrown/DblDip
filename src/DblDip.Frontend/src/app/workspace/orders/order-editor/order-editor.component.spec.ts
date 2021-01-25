import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OrderEditorComponent } from './order-editor.component';

describe('OrderEditorComponent', () => {
  let component: OrderEditorComponent;
  let fixture: ComponentFixture<OrderEditorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OrderEditorComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrderEditorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
