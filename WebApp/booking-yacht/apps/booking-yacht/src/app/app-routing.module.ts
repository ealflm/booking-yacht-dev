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
      { path: 'dashboard', component: HomePagesComponent },
      // {
      //   path: 'dashboard/business-account-form',
      //   component: AccountBusinessFormComponent,
      // },
      {
        path: 'dashboard/business-account-form/:id',
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
      { path: 'ticket', component: TicketComponent },
      {
        path: 'ticket/form/:id',
        component: TicketComponent,
      },
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
