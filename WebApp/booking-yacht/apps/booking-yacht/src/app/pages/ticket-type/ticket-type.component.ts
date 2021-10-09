import { Router } from '@angular/router';
import { TicketTypeService } from './../../services/ticket-type.service';
import { SECONDARY_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-ticket-type',
  templateUrl: './ticket-type.component.html',
  styleUrls: ['./ticket-type.component.scss'],
})
export class TicketTypeComponent implements OnInit {
  status = [
    { id: '', lable: 'Tất cả' },
    { id: '4', lable: 'Chấp nhận' },
    { id: '5', lable: 'Từ chối' },
  ];
  ticketType: [] = [];
  loading = true;
  ticketTypeStatus = SECONDARY_STATUS;
  constructor(
    private ticketTypeService: TicketTypeService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getTicketTypes();
  }
  getTicketTypes() {
    if (!status) {
      this.ticketTypeService.getTicketTypes().subscribe((ticketTypeRes) => {
        setTimeout(() => {
          this.loading = false;
        }, 1000);
        this.ticketType = ticketTypeRes.data;
        console.log(this.ticketType);
      });
    }
  }
  onChangeStatus(status?: string) {
    if (!status) {
      this.ticketTypeService.getTicketTypes().subscribe((ticketTypeRes) => {
        this.ticketType = ticketTypeRes.data;
        setTimeout(() => {
          this.loading = false;
        }, 1000);
      });
    } else {
      this.ticketTypeService
        .getTicketTypes(status)
        .subscribe((ticketTypeRes) => {
          setTimeout(() => {
            this.loading = false;
          }, 1000);
          this.ticketType = ticketTypeRes.data;
        });
    }
  }
  getValue(event: Event) {
    return (event.target as HTMLInputElement).value;
  }
  editTicketType(id: string) {
    this.router.navigate([`ticket-type/form/${id}`]);
  }
}
