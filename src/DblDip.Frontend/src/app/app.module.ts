import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { SharedModule } from './shared/shared.module';
import { CoreModule } from './core/core.module';
import { AppComponent } from './app.component';
import { baseUrl } from './core/constants';
import { HomeComponent } from './public/home/home.component';
import { AdminComponent } from './admin/admin.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    SharedModule,
    CoreModule,
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [{
    provide: baseUrl,
    useValue: 'https://localhost:5001/'
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
