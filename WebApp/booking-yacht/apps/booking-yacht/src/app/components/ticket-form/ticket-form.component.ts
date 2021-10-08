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
  constructor(
    private location: Location,
    private ticketService: TicketsService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.ticketService.getTicket(params.id).subscribe((res) => {
          this.tickets = res.data;
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
