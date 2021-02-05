import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AccountEditorComponent } from './account-editor/account-editor.component';
import { AccountDetailComponent } from './account-detail/account-detail.component';
import { AccountListComponent } from './account-list/account-list.component';
import { AccountService } from './account.service';



@NgModule({
  declarations: [AccountEditorComponent, AccountDetailComponent, AccountListComponent],
  providers: [
    AccountService
  ],
  imports: [
    CommonModule
  ]
})
export class AccountsModule { }
