import { BusinessAccount } from './../../../models/businessAcount';
import { MessageService } from 'primeng/api';
import { BusinessAccountService } from './../../../services/business-account.service';
import { BUSINESS_STATUS } from './../../../constants/BUSINESS_STATUS';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Location } from '@angular/common';

@Component({
  selector: 'booking-yacht-account-business-form',
  templateUrl: './account-business-form.component.html',
  styleUrls: ['./account-business-form.component.scss'],
})
export class AccountBusinessFormComponent implements OnInit {
  businessStatus: any[] = [1, 2];

  selectedStatus: any;
  form!: FormGroup;
  isSubmit = false;
  editMode = false;
  loading = true;
  constructor(
    private formBuider: FormBuilder,
    private businessAccountService: BusinessAccountService,
    private messageService: MessageService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this._initBusinessForm();
    this._mapBusinessStatus();

    setInterval(() => {
      this.loading = false;
    }, 500);
  }

  private _initBusinessForm() {
    this.form = this.formBuider.group({
      name: ['', Validators.required],
      phone: ['', Validators.required],
      email: ['', Validators.required],
      address: ['', Validators.required],
      status: ['', Validators.required],
    });
  }

  _mapBusinessStatus() {
    this.businessStatus = Object.keys(BUSINESS_STATUS).map((key) => {
      return {
        id: key,
        name: BUSINESS_STATUS[key].lable,
      };
    });
  }

  onSubmit() {
    this.isSubmit = true;
    if (this.form.invalid) {
      return;
    }
    const businessAccount: BusinessAccount = {
      name: this.businessForm.name.value,
      phone: this.businessForm.phone.value,
      emailAddress: this.businessForm.email.value,
      address: this.businessForm.address.value,
      status: this.businessForm.status.value,
    };
    console.log(businessAccount);

    this.businessAccountService
      .createBusinessAccount(businessAccount)
      .subscribe((res) => {
        console.log(res);
      });
  }
  onCancle() {
    this.location.back();
  }
  get businessForm() {
    return this.form.controls;
  }
}
