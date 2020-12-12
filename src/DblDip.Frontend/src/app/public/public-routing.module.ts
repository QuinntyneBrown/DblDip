import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  { path: "", component: HomeComponent, pathMatch: "full" },
  // { path: "login", loadChildren: () => import("src/app/public/login/login.module").then(m => m.LoginModule) },
  // { path: "portfolio", loadChildren: () => import("src/app/public/portfolio/portfolio.module").then(m => m.PortfolioModule) },
  // { path: "quote", loadChildren: () => import("src/app/public/quote/quote.module").then(m => m.QuoteModule)},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule {}
