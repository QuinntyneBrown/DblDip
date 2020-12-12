import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AuthGuard } from './_core/auth.guard';

const routes: Routes = [
  { path: "", redirectTo: "public", pathMatch: "full" },
  {
    path: "public",
    loadChildren: () => import("src/app/public/public.module").then(m => m.PublicModule)
  },
  {
    path: "admin",
    component: AppComponent,
    children: [
      {
        path: "",
        canActivate:[AuthGuard],
        loadChildren: () => import("src/app/leads/leads.module").then(m => m.LeadsModule)
      },        
    ],
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
