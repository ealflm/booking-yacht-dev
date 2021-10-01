import { Apartment } from './../../models/apartment_models';
import { ActivatedRoute } from '@angular/router';
import { Route } from '@angular/compiler/src/core';
import { APARTMENT_STATUS } from './../../constants/BUSINESS_STATUS';
import { ApartmentsService } from './../../services/apartments.service';
import { MessageService } from 'primeng/api';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { timer } from 'rxjs';

@Component({
  selector: 'booking-yacht-aparment-form',
  templateUrl: './aparment-form.component.html',
  styleUrls: ['./aparment-form.component.scss'],
})
export class AparmentFormComponent implements OnInit {
  editMode = false;
  loading = true;
  form!: FormGroup;
  isSubmit = false;
  // eslint-disable-next-line @typescript-eslint/ban-types
  status: Array<Object> = [];
  currentUser!: string;
  selected = '1';
  constructor(
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private location: Location,
    private apartmentService: ApartmentsService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this._initForm();
    this._mapApartmentStatus();
    this.checkEditMode();
    setTimeout(() => {
      this.loading = false;
    }, 500);
    // console.log(this.status);
  }
  checkEditMode() {
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.currentUser = params.id;
        this.editMode = true;
        this.apartmentService
          .getApartment(this.currentUser)
          .subscribe((res) => {
            this.apartmentForm.name.setValue(res.name);
            this.apartmentForm.status.setValue(res.status.toString());
          });
      }
    });
  }

  _initForm() {
    this.form = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      name: ['', Validators.required],
      status: ['', Validators.required],
    });
  }
  onSubmit() {
    this.isSubmit = true;
    if (this.apartmentForm.invalid) return;
    if (!this.editMode) {
      const apartment: any = {
        name: this.apartmentForm.name.value,
      };
      this.apartmentService.createApartment(apartment).subscribe(
        (res) => {
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
        },
        (error) => {
          this.messageService.add({
            severity: 'error',
            summary: 'Some thing wrong',
            detail: 'Some filed worng!!',
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        }
      );
    } else {
      const apartment: any = {
        name: this.apartmentForm.name.value,
        status: this.apartmentForm.status.value,
      };
      this.apartmentService
        .updateApartment(apartment, this.currentUser)
        .subscribe(
          (res) => {
            this.messageService.add({
              severity: 'success',
              summary: 'Successfull',
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
              severity: 'error',
              summary: 'Some thing wrong',
              detail: 'Some filed worng!!',
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

  private _mapApartmentStatus() {
    this.status = Object.keys(APARTMENT_STATUS).map((key) => {
      return {
        id: key,
        name: APARTMENT_STATUS[key].lable,
      };
    });
  }
  onCancle() {
    this.location.back();
  }
  get apartmentForm() {
    return this.form.controls;
  }
}
