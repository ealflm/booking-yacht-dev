import { DoashboardComponent } from './pages/doashboard/doashboard.component';
import { TicketFormComponent } from './components/ticket-form/ticket-form.component';
import { TicketComponent } from './pages/ticket/ticket.component';
import { VehicleTypeComponent } from './pages/vehicle-type/vehicle-type.component';
import { TicketTypeComponent } from './pages/ticket-type/ticket-type.component';
import { AgenciesComponent } from './pages/agencies/agencies.component';
import { DestinationsFormComponent } from './components/destinations-form/destinations-form.component';
import { DestinationsComponent } from './pages/destinations/destinations.component';
import { TourFormComponent } from './components/tour-form/tour-form.component';
import { ToursComponent } from './pages/tours/tours.component';
import { AccountBusinessFormComponent } from './components/account-business-form/account-business-form.component';

import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { PagesComponent } from './pages/pages.component';
import { AuthGuardService } from './auth/auth.guard';
import { HomePagesComponent } from './pages/home-pages/home-pages.component';
import { ApartmentsComponent } from './pages/apartments/apartments.component';
import { AparmentFormComponent } from './components/aparment-form/aparment-form.component';
import { AgenciesFormComponent } from './components/agencies-form/agencies-form.component';
import { TicketTypeFormComponent } from './components/ticket-type-form/ticket-type-form.component';
import { VehicleTypeFormComponent } from './components/vehicle-type-form/vehicle-type-form.component';
import { PaymentComponent } from './pages/payment/payment.component';
import { PaymentDetailsComponent } from './pages/payment/payment-details/payment-details.component';

const routes: Routes = [
  {
    path: '',
    component: PagesComponent,
    canActivate: [AuthGuardService],
    children: [
      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full',
      },
      { path: 'dashboard', component: DoashboardComponent },

      { path: 'bussiness', component: HomePagesComponent },

      {
        path: 'bussiness/business-account-form/:id',
        component: AccountBusinessFormComponent,
      },
      { path: 'agencies', component: AgenciesComponent },
      { path: 'agencies/form', component: AgenciesFormComponent },
      { path: 'agencies/form/:id', component: AgenciesFormComponent },

      { path: 'apartments', component: ApartmentsComponent },
      { path: 'apartments/form', component: AparmentFormComponent },
      { path: 'apartments/form/:id', component: AparmentFormComponent },

      { path: 'tours', component: ToursComponent },
      { path: 'tours/form', component: TourFormComponent },
      { path: 'tours/form/:id', component: TourFormComponent },
      { path: 'destinations', component: DestinationsComponent },
      {
        path: 'destinations/form',
        component: DestinationsFormComponent,
      },
      {
        path: 'destinations/form/:id',
        component: DestinationsFormComponent,
      },

      { path: 'ticket-type', component: TicketTypeComponent },
      // {
      //   path: 'ticket-type/form',
      //   component: TicketTypeFormComponent,
      // },
      {
        path: 'ticket-type/form/:id',
        component: TicketTypeFormComponent,
      },

      { path: 'vehicle-type', component: VehicleTypeComponent },
      {
        path: 'vehicle-type/form',
        component: VehicleTypeFormComponent,
      },
      {
        path: 'vehicle-type/form/:id',
        component: VehicleTypeFormComponent,
      },
      { path: 'ticket/:id', component: TicketComponent },
      {
        path: 'ticket/form/:id',
        component: TicketFormComponent,
      },
      { path: 'payment', component: PaymentComponent },
      { path: 'payment/:id', component: PaymentDetailsComponent },
    ],
  },
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forRoot(routes, {
      useHash: true,
      initialNavigation: 'enabled',
    }),
  ],
  exports: [RouterModule],
})
export class AppRoutingModule {}
