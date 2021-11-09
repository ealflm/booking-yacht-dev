import { timer, Subscription } from 'rxjs';
import { TicketType } from './../../models/ticket-types';
import { Router } from '@angular/router';
import { TicketTypeService } from './../../services/ticket-type.service';
import { SECONDARY_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';
import { BusinessAccountService } from '../../services/business-account.service';
import { delay } from 'lodash';

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
  ticketType: TicketType[] = [];
  loading = true;
  subscription$ = new Subscription();
  ticketTypeStatus = SECONDARY_STATUS;
  constructor(
    private ticketTypeService: TicketTypeService,
    private router: Router,
    private business: BusinessAccountService
  ) {}

  ngOnInit(): void {
    this.getTicketTypes();
  }
  getTicketTypes() {
    this.ticketTypeService.getTicketTypes().subscribe((ticketTypeRes) => {
      this.ticketType = ticketTypeRes.data;
      console.log(ticketTypeRes);

      // this.ticketType.map((ticketTypeRes2: any | TicketType) => {
      //   console.log(ticketTypeRes2);
      //   this.business
      //     .getBusinessAccountByID(
      //       ticketTypeRes2.idBusinessTourNavigation?.idBusiness
      //     )
      // .subscribe((bussinessRes) => {
      // console.log(bussinessRes);

      //   ticketTypeRes2.nameBusiness = bussinessRes.data.name;
      // });
      // });
      timer(1000).subscribe(() => {
        this.loading = false;
      });
    });
  }

  onChangeStatus(status: string) {
    if (!status) {
      // console.log(status);

      this.ticketTypeService
        .getTicketTypes(status)
        .subscribe((ticketTypeRes) => {
          this.ticketType = ticketTypeRes.data;
          // this.ticketType.map((ticketTypeRes2: any | TicketType) => {
          //   // console.log(ticketTypeRes2.idBusinessTourNavigation.idBusiness);
          //   this.business
          //     .getBusinessAccountByID(
          //       ticketTypeRes2.idBusinessTourNavigation?.idBusiness
          //     )
          //     .subscribe((bussinessRes) => {
          //       // console.log(bussinessRes);

          //       ticketTypeRes2.nameBusiness = bussinessRes.data.name;
          //     });
          // });
          timer(1000).subscribe(() => {
            this.loading = false;
          });
        });
    } else {
      this.ticketTypeService
        .getTicketTypes(status)
        .subscribe((ticketTypeRes) => {
          timer(1000).subscribe(() => {
            this.loading = false;
          });
          this.ticketType = ticketTypeRes.data;
          // this.ticketType.map((ticketTypeRes2: any | TicketType) => {
          //   // console.log(ticketTypeRes2.idBusinessTourNavigation.idBusiness);
          //   this.business
          //     .getBusinessAccountByID(
          //       ticketTypeRes2.idBusinessTourNavigation?.idBusiness
          //     )
          //     .subscribe((bussinessRes) => {
          //       // console.log(bussinessRes);

          //       ticketTypeRes2.nameBusiness = bussinessRes.data.name;
          //     });
          // });
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
