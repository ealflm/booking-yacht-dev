import {
  ConfirmationService,
  ConfirmEventType,
  MessageService,
} from 'primeng/api';
import { Route } from '@angular/compiler/src/core';
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
    title: 'Đại lý',
    icon: 'ni-circle-08 text-primary',
    class: '',
  },
  {
    path: '/tours',
    title: 'Chuyến đi',
    icon: 'ni-send text-yellow',
    class: '',
  },

  {
    path: '/destinations',
    title: 'Địa điểm du lịch',
    icon: 'ni-app text-red',
    class: '',
  },
];
export const KIND_ROUTES: RouteInfo[] = [
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
  {
    path: '/apartments',
    title: 'Loại địa điểm',
    icon: 'ni-planet text-blue',
    class: '',
  },
  {
    path: '/ticket',
    title: 'Danh sách vé',
    icon: 'ni-badge text-orange',
    class: '',
  },
  {
    path: '/order',
    title: 'Doanh thu',
    icon: 'ni-money-coins text-orange',
    class: '',
  },
];

@Component({
  selector: 'booking-yacht-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class SidebarComponent implements OnInit {
  position?: string;
  public menuItems?: any[];
  public menuItems2?: any[];
  public isCollapsed?: boolean = true;
  colapse?: boolean = true;
  constructor(
    private router: Router,
    private local: LocalStorageService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) {}

  ngOnInit() {
    this.menuItems = ROUTES.filter((menuItem) => menuItem);
    this.menuItems2 = KIND_ROUTES.filter((menuItems2) => {
      menuItems2;
    });
    this.router.events.subscribe((event) => {
      this.isCollapsed = true;
    });
  }
  onChange() {
    this.colapse = !this.colapse;
  }
  logout() {
    this.local.removeToken();
    this.router.navigate(['login']);
  }
}
