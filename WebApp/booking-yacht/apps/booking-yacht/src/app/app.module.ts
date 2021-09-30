import { AuthModule } from './auth/auth.module';
import { PrimengModule } from './primeng/primeng/primeng.module';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HomePagesComponent } from './pages/home-pages/home-pages.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

// import { MaterialModule } from './material/material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

//firebase services
import { ComponentsModule } from './components/components.module';
import { ConfirmationService, MessageService } from 'primeng/api';
import { JwtInterceptor } from './auth/jwt.interceptor';
import { AccountBusinessFormComponent } from './pages/home-pages/account-business-form/account-business-form.component';
import { ManagePlaceComponent } from './pages/manage-place/manage-place.component';
import { PagesComponent } from './pages/pages.component';
import { LoginComponent } from './auth/login/login.component';
@NgModule({
  declarations: [
    AppComponent,
    HomePagesComponent,
    PagesComponent,
    LoginComponent,
    ManagePlaceComponent,
    AccountBusinessFormComponent,
  ],
  imports: [
    // MaterialModule,
    BrowserModule,
    CommonModule,
    BrowserAnimationsModule,
    FormsModule,
    AppRoutingModule,
    ReactiveFormsModule,
    ComponentsModule,
    PrimengModule,
    HttpClientModule,
    AuthModule,
  ],
  providers: [
    MessageService,
    ConfirmationService,
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
  ],
  bootstrap: [AppComponent],
  exports: [],
})
export class AppModule {}
