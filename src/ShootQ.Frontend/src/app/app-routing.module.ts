import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { HomeComponent } from './public/home/home.component';
import { LoginPageComponent } from './public/login/login-page.component';
import { AuthGuard } from './_core/auth.guard';

const routes: Routes = [
  { path: "login", component: LoginPageComponent },
  { path: "", component: HomeComponent, pathMatch: 'full' },
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
