import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '@core/auth.guard';
import { LoginComponent } from './login/login/login.component';
import { NotFoundComponent } from './not-found/not-found/not-found.component';


const routes: Routes = [
  { 
    path:"", 
    pathMatch:"full", 
    redirectTo: "public" 
  },
  { path: "login", component: LoginComponent },  
  { 
    path: "workspace", 
    loadChildren: () => import("src/app/workspace/workspace.module").then(x => x.WorkspaceModule),
    canActivate: [AuthGuard]    
  },
  { 
    path: "public", 
    loadChildren: () => import("src/app/public/public.module").then(x => x.PublicModule)   
  },
  { path: '**', component: NotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
