import { ActivatedRoute, Router } from '@angular/router';
import { BusinessAccount } from '../../models/business-account';
import { MessageService } from 'primeng/api';
import { BusinessAccountService } from '../../services/business-account.service';
import { BUSINESS_STATUS } from '../../constants/STATUS';
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
    this._checkEditMode();
    this._initBusinessForm();
    this._mapBusinessStatus();

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

  private _mapBusinessStatus() {
    this.businessStatus = Object.keys(BUSINESS_STATUS).map((key) => {
      return {
        id: key,
        name: BUSINESS_STATUS[key].lable,
      };
    });
  }

  onSubmit() {
    this.isSubmit = true;
    if (this.form.invalid && this.form.hasValidator) {
      return;
    }
    const businessAccount: BusinessAccount = {
      name: this.businessForm.name.value,
      phoneNumber: this.businessForm.phone.value,
      emailAddress: this.businessForm.email.value,
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
    this.businessAccountService.updateBusinessAccount(id, business).subscribe(
      (res) => {
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
      },
      (error) => {
        this.messageService.add({
          severity: 'error',
          summary: 'Error',
          detail: 'update fail',
        });
        timer(500)
        .toPromise()
        .then(() => {
          this.location.back();
        });
      }
    );

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
            this.businessForm.name.setValue(res.data.name);
            this.businessForm.phone.setValue(res.data.phoneNumber);
            this.businessForm.email.setValue(res.data.emailAddress);
            this.businessForm.address.setValue(res.data.address);
            this.businessForm.status.setValue(res.data.status?.toString());
            this.selectedStatus = res.status;
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
