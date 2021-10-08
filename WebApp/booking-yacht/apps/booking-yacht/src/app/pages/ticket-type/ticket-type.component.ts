import { TOUR_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-ticket-type',
  templateUrl: './ticket-type.component.html',
  styleUrls: ['./ticket-type.component.scss'],
})
export class TicketTypeComponent implements OnInit {
  status = [
    { id: '', lable: 'Tất cả' },
    { id: '1', lable: 'Chấp nhận' },
    { id: '2', lable: 'Từ chối' },
  ];
  ticketType: [] = [];
  loading = true;
  ticketTypeStatus = TOUR_STATUS;
  constructor() {}

  ngOnInit(): void {}
  onChangeStatus(id: string) {}
  getValue(event: Event) {
    return (event.target as HTMLInputElement).value;
  }
}
