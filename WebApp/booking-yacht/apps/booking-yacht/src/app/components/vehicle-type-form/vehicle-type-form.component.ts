import { MessageService } from 'primeng/api';
import { VehicleType } from './../../models/vehicle-types';
import { VehicleTypeService } from './../../services/vehicle-type.service';
import { ActivatedRoute } from '@angular/router';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { SECONDARY_STATUS, AGENCY_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { timer } from 'rxjs';

@Component({
  selector: 'booking-yacht-vehicle-type-form',
  templateUrl: './vehicle-type-form.component.html',
  styleUrls: ['./vehicle-type-form.component.scss'],
})
export class VehicleTypeFormComponent implements OnInit {
  loading = false;
  vehicleType: any = [];
  vehicleTypeStatus: any = [];
  form!: FormGroup;
  isSubmit = false;
  editMode = false;
  currentUser = '';
  constructor(
    private location: Location,
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private vehicleTypeService: VehicleTypeService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this._initForm();
    this._mapBusinessStatus();
    this._checkEditMode();
  }
  onSubmit() {
    this.isSubmit = true;
    if (this.form.invalid && this.form.hasValidator) {
      return;
    }
    const vehicleType: VehicleType = {
      name: this.vehicleForm.name.value,
      status: this.vehicleForm.status.value,
    };
    if (this.editMode) {
      this._updateVehicleType(vehicleType, this.currentUser);
    } else {
      this._newVehicleType(vehicleType);
    }
  }
  private _updateVehicleType(vehicleType: VehicleType, id: string) {
    this.vehicleTypeService.updateVehicle(vehicleType, id).subscribe(
      (res) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Successfull',
          detail: 'Cập nhật thành công',
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
          detail: 'Cập nhật thất bại',
        });
        timer(500)
          .toPromise()
          .then(() => {
            this.location.back();
          });
      }
    );
  }
  private _newVehicleType(vehicleType: VehicleType) {
    this.vehicleTypeService.createVehicle(vehicleType).subscribe((res) => {
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
  onCancle() {
    this.location.back();
  }
  private _mapBusinessStatus() {
    this.vehicleTypeStatus = Object.keys(AGENCY_STATUS).map((key) => {
      return {
        id: key,
        name: AGENCY_STATUS[key].lable,
      };
    });
  }
  _checkEditMode() {
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.editMode = true;
        this.currentUser = params.id;
        this.vehicleTypeService
          .getVehicleType(this.currentUser)
          .subscribe((res) => {
            this.vehicleForm.name.setValue(res.data.name);
            this.vehicleForm.status.setValue(res.data.status?.toString());
          });
      }
    });
  }
  private _initForm() {
    this.form = this.formBuilder.group({
      name: ['', [Validators.required]],
      status: ['', [Validators.required]],
    });
  }
  get vehicleForm() {
    return this.form.controls;
  }
}
