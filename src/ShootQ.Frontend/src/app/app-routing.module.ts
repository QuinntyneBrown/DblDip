import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { LoginPageComponent } from './login/login-page.component';
import { AuthGuard } from './_core/auth.guard';

const routes: Routes = [
  { path: "login", component: LoginPageComponent },
  {
    path: "",
    component: AppComponent,
    children: [
      {
        path: "",
        canActivate:[AuthGuard],
        loadChildren: () => import("src/app/dashboard/dashboard.module").then(m => m.DashboardModule)
      },        
    ],
  }
];


@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
