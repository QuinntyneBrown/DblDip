import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from './core/auth.guard';

const routes: Routes = [
  { path: "", redirectTo: "public", pathMatch: "full" },
  {
    path: "public",
    loadChildren: () => import("src/app/public/public.module").then(m => m.PublicModule)
  },
  {
    path: "workspace",
    loadChildren: () => import("src/app/admin/admin.module").then(m => m.AdminModule),
    canActivate:[AuthGuard]
  }  
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
