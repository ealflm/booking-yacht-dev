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
  constructor(
    private formBuider: FormBuilder,
    private businessAccountService: BusinessAccountService,
    private messageService: MessageService,
    private location: Location
  ) {}

  ngOnInit(): void {
    this._initBusinessForm();
    this._mapBusinessStatus();
  }

  private _initBusinessForm() {
    this.form = this.formBuider.group({
      name: ['', Validators.required],
      phone: [
        '',
        Validators.required,
        Validators.pattern('/(09|01[2|6|8|9])+([0-9]{8})\b/'),
      ],
      email: ['', Validators.required, Validators.email],
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
  onSubmit() {}
  onCancle() {
    this.location.back();
  }
}
