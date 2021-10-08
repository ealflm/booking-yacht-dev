import { Router } from '@angular/router';
import { TicketsService } from './../../services/tickets.service';
import { SECONDARY_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-ticket',
  templateUrl: './ticket.component.html',
  styleUrls: ['./ticket.component.scss'],
})
export class TicketComponent implements OnInit {
  status = [
    { id: '3', lable: 'Chờ duyệt' },
    { id: '4', lable: 'Chấp nhận' },
    { id: '5', lable: 'Từ chối' },
  ];
  ticketsStatus = SECONDARY_STATUS;
  tickets: any[] = [];
  loading = true;
  constructor(private ticketService: TicketsService, private router: Router) {}

  ngOnInit(): void {
    this.getTickets();
  }
  getTickets(status?: string) {
    if (!status) {
      this.ticketService.getTickets().subscribe((ticketResponse) => {
        this.tickets = ticketResponse.data;
        // console.log(this.tickets);

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
