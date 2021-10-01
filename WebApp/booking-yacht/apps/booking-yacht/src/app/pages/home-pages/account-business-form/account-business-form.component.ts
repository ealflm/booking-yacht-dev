import { ActivatedRoute, Router } from '@angular/router';
import { BusinessAccount } from './../../../models/businessAcount';
import { MessageService } from 'primeng/api';
import { BusinessAccountService } from './../../../services/business-account.service';
import { BUSINESS_STATUS } from './../../../constants/BUSINESS_STATUS';
import { Component, OnInit } from '@angular/core';
import {
  EmailValidator,
  FormBuilder,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Location } from '@angular/common';
import { Route } from '@angular/compiler/src/core';
import { timer } from 'rxjs';

@Component({
  selector: 'booking-yacht-account-business-form',
  templateUrl: './account-business-form.component.html',
  styleUrls: ['./account-business-form.component.scss'],
})
export class AccountBusinessFormComponent implements OnInit {
  businessStatus: any[] = [];

  selectedStatus: any;
  form!: FormGroup;
  isSubmit = false;
  editMode = false;
  loading = true;

  currentUser!: string;
  constructor(
    private formBuider: FormBuilder,
    private businessAccountService: BusinessAccountService,
    private messageService: MessageService,
    private location: Location,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    this._initBusinessForm();

    this._mapBusinessStatus();
    this._checkEditMode();
    setTimeout(() => {
      this.loading = false;
    }, 500);
  }

  private _initBusinessForm() {
    this.form = this.formBuider.group({
      name: ['', Validators.required],
      phone: [
        '',
        [
          Validators.required,
          Validators.maxLength(10),
          Validators.minLength(9),
        ],
      ],
      email: [
        { value: '', disabled: true },
        [Validators.required, Validators.email],
      ],
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
      address: this.businessForm.address.value,
      status: this.businessForm.status.value,
    };
    // console.log(businessAccount);
    if (this.editMode) {
      this._updateBusiness(businessAccount, this.currentUser);
    } else {
      this._newBusiness(businessAccount);
    }
  }

  _newBusiness(business: BusinessAccount) {
    this.businessAccountService
      .createBusinessAccount(business)
      .subscribe((res) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Successfull',
          detail: 'New successfull',
        });
        timer(500)
          .toPromise()
          .then(() => {
            this.location.back();
          });
      });
  }

  private _updateBusiness(business: BusinessAccount, id: string) {
    this.businessAccountService
      .updateBusinessAccount(id, business)
      .subscribe((res) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Successfull',
          detail: 'update successfull',
        });
        timer(500)
          .toPromise()
          .then(() => {
            this.location.back();
          });
      });
  }

  _checkEditMode() {
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.editMode = true;
        this.currentUser = params.id;
        // console.log(this.currentUser);
        this.businessAccountService
          .getBusinessAccountByID(this.currentUser)
          .subscribe((res) => {
            this.businessForm.name.setValue(res.name);
            this.businessForm.phone.setValue(res.phone);
            this.businessForm.email.setValue(res.emailAddress);
            this.businessForm.address.setValue(res.address);
            this.businessForm.status.setValue(res.status);
            this.selectedStatus = res.status;
            // console.log(this.selectedStatus);
          });
      }
    });
  }
  onCancle() {
    this.location.back();
  }
  get businessForm() {
    return this.form.controls;
  }
}
