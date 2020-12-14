import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { PublicComponent } from "./public/public.component";
import { HomeComponent } from "./home/home.component";
import { TestimonialsComponent } from "./testimonials/testimonials/testimonials.component";
import { AboutComponent } from "./about/about/about.component";
import { PortfolioComponent } from "./portfolio/portfolio/portfolio.component";
import { PricingComponent } from "./pricing/pricing/pricing.component";
import { ContactComponent } from "./contact/contact/contact.component";

const routes: Routes = [
  { 
    path: "", 
    component: PublicComponent,
    children: [
      { path: "", component: HomeComponent },
      { path: "about", component: AboutComponent },
      { path: "contact", component: ContactComponent },
      { path: "portfolio", component: PortfolioComponent },
      { path: "pricing", component: PricingComponent },
      { path: "testimonials", component: TestimonialsComponent }
    ]
   },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule {}
