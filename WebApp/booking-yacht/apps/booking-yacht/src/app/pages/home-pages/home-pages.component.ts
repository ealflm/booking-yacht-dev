import { Router } from '@angular/router';
import { ConfirmationService, MessageService } from 'primeng/api';
import { BUSINESS_STATUS } from './../../constants/BUSINESS_STATUS';
import { BusinessAccount } from './../../models/businessAcount';
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
  constructor(
    private businessService: BusinessAccountService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getBusinessAccount();
  }

  private getBusinessAccount() {
    this.businessService.getBusinessAccount().subscribe((responseData) => {
      this.businessAcount = responseData;
      // console.log(this.businessAcount);

      setInterval(() => {
        this.loading = false;
      }, 1000);
    });
  }

  editBusiness(id: string) {}
  deleteBusiness(id: string) {
    this.confirmationService.confirm({
      message: 'Are you sure want to delete?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.businessService.deleteBusinessAccount(id).subscribe((response) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Successfull',
            detail: 'Delete successfull',
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
