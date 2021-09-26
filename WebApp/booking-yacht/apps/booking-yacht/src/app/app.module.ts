import { PrimengModule } from './primeng/primeng/primeng.module';
import { UsersService } from './services/users.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HomePagesComponent } from './pages/home-pages/home-pages.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { PagesComponent } from './pages/pages.component';
import { LoginComponent } from './auth/login/login.component';
import { PagesRoutingModule } from './pages/pages-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MaterialModule } from './material/material.module';
import { ComponentsModule } from './components/components.module';
import { ManagePlaceComponent } from './pages/manage-place/manage-place.component';
@NgModule({
  declarations: [
    AppComponent,
    HomePagesComponent,
    PagesComponent,
    LoginComponent,
    ManagePlaceComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    PagesRoutingModule,
    BrowserAnimationsModule,
    // MaterialModule,
    ComponentsModule,
    PrimengModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [],
})
export class AppModule {}
