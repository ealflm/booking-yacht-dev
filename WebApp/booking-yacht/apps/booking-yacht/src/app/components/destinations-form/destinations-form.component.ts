import { HttpErrorResponse } from '@angular/common/http';
import { MessageService } from 'primeng/api';
import { STATUS } from './../../constants/STATUS';
import { Desti, Destination } from './../../models/destinations';
import { ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ApartmentsService } from './../../services/apartments.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { DestinationsService } from '../../services/destinations.service';
import { timer } from 'rxjs';
import { identity } from 'lodash';
@Component({
  selector: 'booking-yacht-destinations-form',
  templateUrl: './destinations-form.component.html',
  styleUrls: ['./destinations-form.component.scss'],
})
export class DestinationsFormComponent implements OnInit {
  editMode = false;
  loading = false;
  form!: FormGroup;
  isSubmit = false;
  desStatus: any[] = [];
  status = STATUS;
  currentUser!: string;
  constructor(
    private apartmentService: ApartmentsService,
    private formBuilder: FormBuilder,
    private location: Location,
    private route: ActivatedRoute,
    private desService: DestinationsService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this._initForm();
    this.getPlaceType();
    this._checkEditMode();
  }

  getPlaceType() {
    this.apartmentService.getApartments().subscribe((apartmentRes) => {
      this.desStatus = apartmentRes.data;
    });
  }
  _initForm() {
    this.form = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      address: ['', [Validators.required]],
      idPlaceType: ['', [Validators.required]],
      status: ['', [Validators.required]],
      name: ['', [Validators.required]],
    });
  }
  private _checkEditMode() {
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.editMode = true;
        this.currentUser = params.id;
        this.desService.getDes(this.currentUser).subscribe((desData: any) => {
          // console.log(desData);
          this.desForm.id.setValue(desData.id);
          this.desForm.address.setValue(desData.data.address);
          this.desForm.idPlaceType.setValue(desData.data.idPlaceType);
          this.desForm.status.setValue(desData.data.status.toString());
          this.desForm.name.setValue(desData.data.name);

          // console.log(desData.data.idPlaceType);
        });
      }
    });
  }
  onSubmit() {
    this.isSubmit = true;
    if (this.form.invalid && this.form.hasValidator) {
      return;
    }
    if (this.editMode) {
      const des: Desti = {
        id: this.currentUser,
        address: this.desForm.address.value,
        idPlaceType: this.desForm.idPlaceType.value,
        status: this.desForm.status.value,
        name: this.desForm.name.value,
      };
      this.desService.updateDes(des, this.currentUser).subscribe(
        (response) => {
          this.messageService.add({
            severity: 'success',
            summary: 'SUCCESS',
            detail: 'Update successfull',
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        },
        (error) => {
          this.messageService.add({
            severity: 'success',
            summary: 'ERROR',
            detail: error,
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        }
      );
    } else if (!this.editMode) {
      const des: Desti = {
        id: this.currentUser,
        name: this.desForm.name.value,
        address: this.desForm.address.value,
        idPlaceType: this.desForm.idPlaceType.value,
        status: this.desForm.status.value,
      };
      this.desService.createDes(des).subscribe(
        (response) => {
          this.messageService.add({
            severity: 'success',
            summary: 'SUCCESS',
            detail: 'Create successfull',
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        },
        (error) => {
          this.messageService.add({
            severity: 'success',
            summary: 'ERROR',
            detail: error,
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        }
      );
    }
  }
  onCancle() {
    this.location.back();
  }
  get desForm() {
    return this.form.controls;
  }
}
