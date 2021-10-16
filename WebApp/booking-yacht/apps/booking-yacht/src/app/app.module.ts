import { TicketFormComponent } from './components/ticket-form/ticket-form.component';
import { TourFormComponent } from './components/tour-form/tour-form.component';
import { AparmentFormComponent } from './components/aparment-form/aparment-form.component';
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
import { AccountBusinessFormComponent } from './components/account-business-form/account-business-form.component';
import { PagesComponent } from './pages/pages.component';
import { LoginComponent } from './auth/login/login.component';
import { ApartmentsComponent } from './pages/apartments/apartments.component';
import { ToursComponent } from './pages/tours/tours.component';
import { DestinationsComponent } from './pages/destinations/destinations.component';
import { AgenciesComponent } from './pages/agencies/agencies.component';
import { TicketTypeComponent } from './pages/ticket-type/ticket-type.component';
import { VehicleTypeComponent } from './pages/vehicle-type/vehicle-type.component';
import { DestinationsFormComponent } from './components/destinations-form/destinations-form.component';
import { AgenciesFormComponent } from './components/agencies-form/agencies-form.component';
import { VehicleTypeFormComponent } from './components/vehicle-type-form/vehicle-type-form.component';
import { TicketTypeFormComponent } from './components/ticket-type-form/ticket-type-form.component';
import { TicketComponent } from './pages/ticket/ticket.component';
import { NavMenuComponent } from './shared/nav-menu/nav-menu.component';
import { VehicleComponent } from './shared/vehicle/vehicle.component';
import { DoashboardComponent } from './pages/doashboard/doashboard.component';
@NgModule({
  declarations: [
    AppComponent,
    HomePagesComponent,
    PagesComponent,
    LoginComponent,
    AccountBusinessFormComponent,
    ApartmentsComponent,
    AparmentFormComponent,
    ToursComponent,
    TourFormComponent,
    DestinationsComponent,
    AgenciesComponent,
    TicketTypeComponent,
    VehicleTypeComponent,
    DestinationsFormComponent,
    AgenciesFormComponent,
    VehicleTypeFormComponent,
    TicketTypeFormComponent,
    TicketComponent,
    NavMenuComponent,
    TicketFormComponent,
    VehicleComponent,
    DoashboardComponent
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
