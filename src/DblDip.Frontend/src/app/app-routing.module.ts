import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: "", redirectTo: "public", pathMatch: "full" },
  {
    path: "public",
    loadChildren: () => import("src/app/public/public.module").then(m => m.PublicModule)
  },
  {
    path: "admin",
    loadChildren: () => import("src/app/admin/admin.module").then(m => m.AdminModule)
  }  
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
