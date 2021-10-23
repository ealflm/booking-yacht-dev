import { SCAN_STATUS } from './../../constants/STATUS';
import { VehicleTypeService } from './../../services/vehicle-type.service';
import { async } from '@angular/core/testing';
import { ActivatedRoute } from '@angular/router';
import { TicketsService } from './../../services/tickets.service';
import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { isBuffer } from 'lodash';

@Component({
  selector: 'booking-yacht-ticket-form',
  templateUrl: './ticket-form.component.html',
  styleUrls: ['./ticket-form.component.scss'],
})
export class TicketFormComponent implements OnInit {
  loading = true;
  tickets: any;
  typeVehicle: any;
  ticketsStatus = SCAN_STATUS;
  constructor(
    private location: Location,
    private ticketService: TicketsService,
    private route: ActivatedRoute,
    private vehicleService: VehicleTypeService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.ticketService.getTicket(params.id).subscribe(async (res) => {
          this.tickets = await res.data;
          console.log(this.tickets);
          this.vehicleService
            .getVehicleType(
              res.data.idTripNavigation.idVehicleNavigation.idVehicleType
            )
            .subscribe(async (typeVehicle) => {
              this.typeVehicle = await typeVehicle.data;
            });
          setTimeout(() => {
            this.loading = false;
          }, 1000);
        });
      }
    });
  }

  onCancle() {
    this.location.back();
  }
}
