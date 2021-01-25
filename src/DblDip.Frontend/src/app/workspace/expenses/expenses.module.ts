import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ExpenseEditorComponent } from './expense-editor/expense-editor.component';



@NgModule({
  declarations: [ExpenseEditorComponent],
  imports: [
    CommonModule
  ]
})
export class ExpensesModule { }
