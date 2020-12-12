import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { LeadsComponent } from './leads/leads.component';

const routes: Routes = [
  { path: "", component: LeadsComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LeadsRoutingModule {}
