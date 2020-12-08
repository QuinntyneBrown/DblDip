import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './_shared/shared.module';
import { CoreModule } from './_core/core.module';
import { AppContainerComponent } from './app-container.component';
import { LoginModule } from './login/login.module';
import { HomeModule } from './home/home.module';
import { baseUrl } from './_core/constants';

@NgModule({
  declarations: [
    AppComponent,
    AppContainerComponent
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
    useValue: 'https://localhost:44348/'
  }],
  bootstrap: [AppContainerComponent]
})
export class AppModule { }
