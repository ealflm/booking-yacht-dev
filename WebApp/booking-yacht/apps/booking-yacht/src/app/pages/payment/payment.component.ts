import { async } from '@angular/core/testing';
import { PaymentService } from './../../services/payment.service';
import { BusinessAccountService } from './../../services/business-account.service';
import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { DomSanitizer } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'booking-yacht-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss'],
})
export class PaymentComponent implements OnInit {
  businessPayment: any;
  loading = false;
  ipAddress = '';
  currentID = '';
  constructor(
    private businessService: BusinessAccountService,
    private paymentService: PaymentService,
    private sanitizer: DomSanitizer,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    const currentMonth = moment(new Date()).format('YYYY-MM');
    // console.log(currentMonth);
    this.getIPAdress();
    this.businessService.getBussinessPayment(currentMonth).subscribe((res) => {
      this.businessPayment = res.data;
      console.log(res);
    });
    this.route.params.subscribe((params) => {
      const paramsID = params.split('?');
      this.currentID = paramsID[0];
      console.log(this.currentID);
    });
  }
  confirmPayment(id: string, totalPrice: number) {
    this.paymentService
      .getPayment(this.ipAddress, totalPrice, id)
      .subscribe((resURL: any) => {
        window.location.href = resURL.data;
        console.log(resURL);
      });
  }
  getIPAdress() {
    this.businessService.getIpAdrress().subscribe((res: any) => {
      this.ipAddress = res.ip;
      console.log(this.ipAddress);
    });
  }
  getValue(event: Event) {
    return (event.target as HTMLInputElement).value;
  }
}
