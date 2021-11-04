import { VehicleService } from './../../services/vehicle.service';
import { TicketsService } from './../../services/tickets.service';
import { AgenciesService } from './../../services/agencies.service';
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
  customter: any[] = [];
  countAgency = 0;
  countOrder = 0;
  countTicket = 0;
  countVehicle = 0;

  constructor(
    private agencyService: AgenciesService,
    private orderService: OrdersService,
    private ticketService: TicketsService,
    private vehicleService: VehicleService
  ) {}

  ngOnInit(): void {
    this.getCustomersRecent();
    this.orderService.getAllOrders().subscribe((orderRes) => {
      this.order = orderRes.data;
      // console.log(orderRes);

      timer(1000).subscribe(() => (this.loading = false));
    });
    this.agencyService.countAgency().subscribe((res) => {
      this.countAgency = res.data;
    });
    this.orderService.countOrder().subscribe((res) => {
      this.countOrder = res.data;
    });
    this.ticketService.countTicket().subscribe((res) => {
      this.countTicket = res.data;
    });
    this.vehicleService.countVehicle().subscribe((res) => {
      this.countVehicle = res.data;
    });
  }
  getCustomersRecent() {
    this.orderService.getCustomerRecent(11).subscribe((customerRes) => {
      this.customter = customerRes.data;
    });
  }
  activeCard(id: any) {
    this.selectedIndex = id;
  }
}
