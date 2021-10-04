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
    title: 'Chủ tàu',
    icon: 'ni-tv-2 text-primary',
    class: '',
  },
  {
    path: '/agencies',
    title: 'Đại Lý',
    icon: 'ni-circle-08 text-primary',
    class: '',
  },
  {
    path: '/apartments',
    title: 'Loại địa điểm',
    icon: 'ni-planet text-blue',
    class: '',
  },
  {
    path: '/destinations',
    title: 'Địa điểm du lịch',
    icon: 'ni-app text-red',
    class: '',
  },

  {
    path: '/tours',
    title: 'chuyến đi',
    icon: 'ni-send text-yellow',
    class: '',
  },
  {
    path: '/vehicle-types',
    title: 'Loại tàu',
    icon: 'ni-delivery-fast text-orange',
    class: '',
  },
  {
    path: '/ticket-types',
    title: 'Loại vé',
    icon: 'ni-paper-diploma text-info',
    class: '',
  },
];

@Component({
  selector: 'booking-yacht-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
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
