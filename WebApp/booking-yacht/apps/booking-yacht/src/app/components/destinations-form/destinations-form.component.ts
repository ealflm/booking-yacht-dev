import { Location } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ApartmentsService } from './../../services/apartments.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
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
  status: any[] = [];
  constructor(
    private apartmentService: ApartmentsService,
    private formBuilder: FormBuilder,
    private location: Location
  ) {}

  ngOnInit(): void {
    this._initForm();
    this.apartmentService.getApartments().subscribe((apartmentRes) => {
      this.status = apartmentRes.data;
    });
  }
  _initForm() {
    this.form = this.formBuilder.group({
      id: [{ value: '', disabled: true }],
      address: ['', [Validators.required]],
      idPlaceType: ['', [Validators.required]],
    });
  }
  onSubmit() {}
  onCancle() {
    this.location.back();
  }
  get desForm() {
    return this.form.controls;
  }
}
