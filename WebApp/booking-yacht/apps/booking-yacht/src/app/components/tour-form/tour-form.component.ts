import { TOUR_STATUS } from './../../constants/STATUS';
import { timer } from 'rxjs';
import { Tour } from './../../models/tours';
import { ActivatedRoute } from '@angular/router';
import { ToursService } from './../../services/tours.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { MessageService } from 'primeng/api';

@Component({
  selector: 'booking-yacht-tour-form',
  templateUrl: './tour-form.component.html',
  styleUrls: ['./tour-form.component.scss'],
})
export class TourFormComponent implements OnInit {
  editMode = false;
  loading = true;
  form!: FormGroup;
  isSubmit = false;
  status: any[] = [];
  currentUser!: string;
  tour: [] = [];
  constructor(
    private formBuilder: FormBuilder,
    private tourService: ToursService,
    private route: ActivatedRoute,
    private location: Location,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this._initForm();
    this._checkEditMode();
    this._mapTourStatus();
    setTimeout(() => {
      this.loading = false;
    }, 500);
  }
  _checkEditMode() {
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.currentUser = params.id;
        this.editMode = true;
        this.tourService.getTour(this.currentUser).subscribe((res) => {
          this.tourForm.id.setValue(res.data.id);
          this.tourForm.tittle.setValue(res.data.tittle);
          this.tourForm.status.setValue(res.data.status.toString());
          this.tourForm.descriptions.setValue(res.data.descriptions);
        });
      }
    });
  }
  _initForm() {
    this.form = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      tittle: ['', [Validators.required]],
      descriptions: ['', Validators.required],
      status: [''],
    });
  }
  _mapTourStatus() {
    this.status = Object.keys(TOUR_STATUS).map((key) => {
      return {
        id: key,
        name: TOUR_STATUS[key].lable,
      };
    });
  }
  onSubmit() {
    this.isSubmit = true;
    if (this.form.invalid) {
      return;
    }

    if (!this.editMode) {
      const tour: Tour = {
        tittle: this.tourForm.tittle.value,
        status: this.tourForm.status.value,
        descriptions: this.tourForm.descriptions.value,
      };
      console.log(tour);

      this.tourService.createTour(tour).subscribe(
        (res) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Update tour successfull',
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
            summary: 'Some thing wrong!',
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        }
      );
    } else {
      const tour: Tour = {
        tittle: this.tourForm.tittle.value,
        status: this.tourForm.status.value,
        descriptions: this.tourForm.descriptions.value,
      };
      console.log(tour);

      this.tourService.updateTour(tour, this.currentUser).subscribe(
        (res) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Update tour successfull',
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
            summary: 'Some thing wrong!',
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
  get tourForm() {
    return this.form.controls;
  }
}
