import { async } from '@angular/core/testing';
import { PaymentService } from './../../services/payment.service';
import { BusinessAccountService } from './../../services/business-account.service';
import { Component, OnInit } from '@angular/core';
import * as moment from 'moment';
import { DomSanitizer } from '@angular/platform-browser';

@Component({
  selector: 'booking-yacht-payment',
  templateUrl: './payment.component.html',
  styleUrls: ['./payment.component.scss'],
})
export class PaymentComponent implements OnInit {
  businessPayment: any;
  loading = false;
  ipAddress = '';
  constructor(
    private businessService: BusinessAccountService,
    private paymentService: PaymentService,
    private sanitizer: DomSanitizer
  ) {}

  ngOnInit(): void {
    const currentMonth = moment(new Date()).format('YYYY-MM');
    // console.log(currentMonth);
    this.getIPAdress();
    this.businessService.getBussinessPayment(currentMonth).subscribe((res) => {
      this.businessPayment = res.data;
      console.log(res);
    });
  }
  confirmPayment(id: string, totalPrice: number) {
    this.paymentService
      .getPayment(this.ipAddress, totalPrice, id)
      .subscribe(async (resURL: any) => {
        window.open(resURL.data);
        await console.log(resURL);
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
