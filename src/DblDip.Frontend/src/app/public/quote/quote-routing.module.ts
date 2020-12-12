import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { QuoteComponent } from './quote/quote.component';

const routes: Routes = [
  { path: "", component: QuoteComponent },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class QuoteRoutingModule {}
