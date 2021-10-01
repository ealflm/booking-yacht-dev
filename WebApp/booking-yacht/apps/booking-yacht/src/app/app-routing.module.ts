import { AccountBusinessFormComponent } from './components/account-business-form/account-business-form.component';
import { LoginComponent } from './auth/login/login.component';

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
