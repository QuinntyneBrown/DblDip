import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { PublicComponent } from "./public.component";

const routes: Routes = [
  {
    path:"",
    component: PublicComponent,
    children:[
      { path: "", loadChildren: () => import("src/app/public/home/home.module").then(m => m.HomeModule)},
      { path: "portfolio", loadChildren: () => import("src/app/public/portfolio/portfolio.module").then(m => m.PortfolioModule) },
      { path: "login", loadChildren: () => import("src/app/public/login/login.module").then(m => m.LoginModule) },
      { path: "quote", loadChildren: () => import("src/app/public/quote/quote.module").then(m => m.QuoteModule) }
    ]  
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule {}
