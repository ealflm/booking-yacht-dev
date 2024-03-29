import { BUSINESS_STATUS } from './../../constants/STATUS';
import { map } from 'rxjs/operators';
import { pipe } from 'rxjs';
import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { BusinessAccount } from '../../models/business-account';
import { UsersService } from './../../services/users.service';

import { Component, OnInit } from '@angular/core';
import { BusinessAccountService } from '../../services/business-account.service';

@Component({
  selector: 'booking-yacht-home-pages',
  templateUrl: './home-pages.component.html',
  styleUrls: ['./home-pages.component.scss'],
})
export class HomePagesComponent implements OnInit {
  businessAcount: BusinessAccount[] = [];
  loading?: boolean = true;
  businessStatus = BUSINESS_STATUS;
  status = [
    { id: '0', lable: 'Tất cả' },
    { id: '1', lable: 'Đang hoạt động' },
    { id: '2', lable: 'Đã vô hiệu hóa' },
  ];
  selectedID?: string;
  constructor(
    private businessService: BusinessAccountService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getBusinessAccount();
    // this._mapBusinessStatus();

    // console.log(this.status);
  }

  onChangeStatus(id: string) {
    this.loading = true;
    this.businessService.getBusinessAccount(id).subscribe((res) => {
      setTimeout(() => {
        this.loading = false;
      }, 1000);
      this.businessAcount = res.data;
    });
  }
  private getBusinessAccount() {
    this.businessService.getBusinessAccount('0').subscribe((res) => {
      // console.log(res);
      this.businessAcount = res.data;
      setTimeout(() => {
        this.loading = false;
      }, 1000);
    });
  }
  // _mapBusinessStatus() {
  //   this.status = Object.keys(BUSINESS_STATUS).map((key) => {
  //     return {
  //       id: key,
  //       name: BUSINESS_STATUS[key].lable,
  //     };
  //   });
  // }
  editBusiness(id: string) {
    return this.router.navigate([`bussiness/business-account-form/${id}`]);
  }
  deleteBusiness(id: string) {
    this.confirmationService.confirm({
      message: 'Are you sure want to disable user ?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.businessService.deleteBusinessAccount(id).subscribe((response) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Successfull',
            detail: 'Update successfull',
          });
          this.getBusinessAccount();
        });
      },
    });
  }
  newBusiness() {
    this.router.navigate(['dashboard/business-account-form']);
  }
  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }
}
