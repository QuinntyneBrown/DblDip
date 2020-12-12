import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { AdminComponent } from './admin.component';

const routes: Routes = [
  {
    path: "",
    pathMatch: "full",
    component: AdminComponent,
    children: [
      {
        path: "",
        pathMatch: "full",
        loadChildren: () => import("src/app/admin/leads/leads.module").then(m => m.LeadsModule)
      },        
    ],
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {}
