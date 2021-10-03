import { DestinationsToursComponent } from './pages/destinations-tours/destinations-tours.component';
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
      {
        path: 'dashboard/business-account-form',
        component: AccountBusinessFormComponent,
      },
      {
        path: 'dashboard/business-account-form/:id',
        component: AccountBusinessFormComponent,
      },
      { path: 'apartments', component: ApartmentsComponent },
      { path: 'apartments/form', component: AparmentFormComponent },
      { path: 'apartments/form/:id', component: AparmentFormComponent },
      { path: 'tours', component: ToursComponent },
      { path: 'tours/form', component: TourFormComponent },
      { path: 'tours/form/:id', component: TourFormComponent },
      { path: 'destinations-tours', component: DestinationsToursComponent },
      {
        path: 'destinations-tours/form',
        component: DestinationsToursComponent,
      },
      {
        path: 'destinations-tours/form/:id',
        component: DestinationsToursComponent,
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
