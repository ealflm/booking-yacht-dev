import { LocalStorageService } from './../../auth/localstorage.service';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

declare interface RouteInfo {
  path: string;
  title: string;
  icon: string;
  class: string;
}
export const ROUTES: RouteInfo[] = [
  {
    path: '/dashboard',
    title: 'Dashboard',
    icon: 'ni-tv-2 text-primary',
    class: '',
  },
  {
    path: '/apartments',
    title: 'Apartments',
    icon: 'ni-planet text-blue',
    class: '',
  },
  {
    path: '/desination-tour',
    title: 'Desinations',
    icon: 'ni-app text-red',
    class: '',
  },
  {
    path: '/user-profile',
    title: 'User profile',
    icon: 'ni-single-02 text-yellow',
    class: '',
  },
  { path: '/maps', title: 'Maps', icon: 'ni-pin-3 text-orange', class: '' },

  { path: '/login', title: 'Login', icon: 'ni-key-25 text-info', class: '' },
  {
    path: '/register',
    title: 'Register',
    icon: 'ni-circle-08 text-pink',
    class: '',
  },
];

@Component({
  selector: 'booking-yacht-sidebar',
  templateUrl: './sidebar.component.html',
})
export class SidebarComponent implements OnInit {
  public menuItems?: any[];
  public isCollapsed = true;

  constructor(private router: Router, private local: LocalStorageService) {}

  ngOnInit() {
    this.menuItems = ROUTES.filter((menuItem) => menuItem);
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
    });
  }
  logout() {
    this.local.removeToken();
  }
}
