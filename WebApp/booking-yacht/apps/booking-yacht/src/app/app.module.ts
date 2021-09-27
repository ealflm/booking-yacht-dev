import { AuthModule } from './auth/auth.module';
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
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MaterialModule } from './material/material.module';
import { ComponentsModule } from './components/components.module';
import { ManagePlaceComponent } from './pages/manage-place/manage-place.component';

import { FormsModule, ReactiveFormsModule } from '@angular/forms';

//firebase services

import { environment } from '../environments/environment';
import { CommonModule } from '@angular/common';
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
    CommonModule,
    BrowserAnimationsModule,
    // MaterialModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    ComponentsModule,
    PrimengModule,
    HttpClientModule,
    AuthModule
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [],
})
export class AppModule {}
