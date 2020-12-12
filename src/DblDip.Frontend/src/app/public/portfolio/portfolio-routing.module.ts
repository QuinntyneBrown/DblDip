import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { PortfolioComponent } from './portfolio.component';

const routes: Routes = [
  { path: "portfolio", component: PortfolioComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PortfolioRoutingModule {}
