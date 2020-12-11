import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './_shared/shared.module';
import { CoreModule } from './_core/core.module';
import { AppContainerComponent } from './app-container.component';
import { LoginModule } from './public/login/login.module';
import { HomeModule } from './public/home/home.module';
import { baseUrl } from './_core/constants';
import { HeaderComponent } from './public/_shared/header/header.component';
import { PortfolioComponent } from './public/portfolio/portfolio.component';

@NgModule({
  declarations: [
    AppComponent,
    AppContainerComponent,
    HeaderComponent,
    PortfolioComponent
  ],
  imports: [
    SharedModule,
    CoreModule,
    HomeModule,
    LoginModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [{
    provide: baseUrl,
    useValue: 'https://localhost:5001/'
  }],
  bootstrap: [AppContainerComponent]
})
export class AppModule { }
