import { PrimengModule } from './primeng/primeng/primeng.module';
import { UsersService } from './services/users.service';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { HomePagesComponent } from './pages/home-pages/home-pages.component';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { FooterComponent } from './shared/footer/footer.component';
import { HeaderComponent } from './shared/header/header.component';
import { PagesComponent } from './pages/pages.component';
import { LoginComponent } from './auth/login/login.component';
import { RegisterComponent } from './auth/register/register.component';
import { PagesRoutingModule } from './pages/pages-routing.module';
import { GalleryComponent } from './pages/gallery/gallery.component';
import { AboutUsComponent } from './pages/about-us/about-us.component';
import { ServicesComponent } from './pages/services/services.component';
import { DestinationsComponent } from './pages/destinations/destinations.component';
import { DestinationsDetailsComponent } from './pages/destinations/destinations-details/destinations-details.component';
import { BookingNowComponent } from './pages/booking-now/booking-now.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MaterialModule } from './material/material.module';
@NgModule({
  declarations: [
    AppComponent,
    FooterComponent,
    HeaderComponent,
    HomePagesComponent,
    PagesComponent,
    LoginComponent,
    RegisterComponent,
    GalleryComponent,
    AboutUsComponent,
    ServicesComponent,
    DestinationsComponent,
    DestinationsDetailsComponent,
    BookingNowComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    PagesRoutingModule,
    BrowserAnimationsModule,
    // MaterialModule,
    PrimengModule,
    HttpClientModule,
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [],
})
export class AppModule {}
