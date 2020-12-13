import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PortfolioFeaturedComponent } from './portfolio-featured/portfolio-featured.component';
import { PortfolioGalleryComponent } from './portfolio-gallery/portfolio-gallery.component';
import { PortfolioComponent } from './portfolio/portfolio.component';



@NgModule({
  declarations: [PortfolioFeaturedComponent, PortfolioGalleryComponent, PortfolioComponent],
  imports: [
    CommonModule
  ]
})
export class PortfolioModule { }
