import { timer } from 'rxjs';
import { OrdersService } from './../../services/orders.service';
import { ORDER_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-doashboard',
  templateUrl: './doashboard.component.html',
  styleUrls: ['./doashboard.component.scss'],
})
export class DoashboardComponent implements OnInit {
  order: any[] = [];
  selectedIndex?: number = 1;
  loading = true;
  orderStatus = ORDER_STATUS;
  constructor(private orderService: OrdersService) {}

  ngOnInit(): void {
    this.orderService.getAllOrders().subscribe((orderRes) => {
      this.order = orderRes.data;
      timer(1000).subscribe(() => (this.loading = false));
    });
  }
  activeCard(id: any) {
    this.selectedIndex = id;
  }
}
