import { Router, ActivatedRoute } from '@angular/router';
import { TicketsService } from './../../services/tickets.service';
import { SECONDARY_STATUS, SCAN_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.scss'],
})
export class TicketComponent implements OnInit {
  ticketsStatus = SCAN_STATUS;
  tickets: any[] = [];
  loading = true;

  constructor(
    private ticketService: TicketsService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.getTickets(params.id);
      }
    });
  }
  getTickets(id: string) {
    if (id) {
      this.ticketService.getTickets(id).subscribe((ticketResponse) => {
        this.tickets = ticketResponse.data;
        console.log(this.tickets);

        setTimeout(() => {
          this.loading = false;
        }, 1000);
      });
    }
  }
  onChangeStatus(status?: string) {}
  getValue(event: Event) {
    return (event.target as HTMLInputElement).value;
  }
}
