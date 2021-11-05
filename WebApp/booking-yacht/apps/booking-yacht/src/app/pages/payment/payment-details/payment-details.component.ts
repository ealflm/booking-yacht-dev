import * as moment from 'moment';
import { PaymentService } from './../../../services/payment.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { BusinessAccountService } from '../../../services/business-account.service';

@Component({
  selector: 'booking-yacht-payment-details',
  templateUrl: './payment-details.component.html',
  styleUrls: ['./payment-details.component.scss'],
})
export class PaymentDetailsComponent implements OnInit {
  currentID = '68554b5a-817b-453c-992c-149662a8e710';
  constructor(
    private route: ActivatedRoute,
    private paymentService: PaymentService,
    private router: Router,
    private bussinessService: BusinessAccountService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.currentID = params.id;
    });
    this.route.queryParams.subscribe((params) => {
      const vnpOrderInfo = params.vnp_OrderInfo;
      const currentMonth = moment(new Date()).format('YYYY-MM');
      if (
        this.currentID == params.vnp_OrderInfo &&
        params.vnp_ResponseCode != 24
      ) {
        this.router.navigate(
          [`payment/${this.currentID}`, { relativeTo: this.route }]
          // `https://www.bookingyacht.site/#/payment/${this.currentID}`
        );
        this.getBussinessID(vnpOrderInfo, currentMonth);
      } else if (params.vnp_ResponseCode == 24) {
        this.router.navigate([`payment`]);
      }
    });
  }
  getBussinessID(id: string, date: string) {
    this.bussinessService.getPaymentID(id, date).subscribe((res) => {
      console.log(res.data);
      res.data.businessTours.map((businessRes: any) => {
        console.log('Business:', businessRes);
        businessRes.trips?.map((tripRes: any) => {
          console.log('tripRes:', tripRes);
          tripRes.orders?.map((order: any) => {
            console.log('Order:', order.id);
          });
        });
      });
    });
  }
}
