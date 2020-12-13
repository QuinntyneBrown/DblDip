import { RouterModule, Routes } from "@angular/router";
import { NgModule } from "@angular/core";
import { PublicComponent } from "./public/public.component";
import { HomeComponent } from "./home/home.component";

const routes: Routes = [
  { 
    path: "", 
    component: PublicComponent,
    children: [
      { path: "", component: HomeComponent }
    ]
   },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule {}
