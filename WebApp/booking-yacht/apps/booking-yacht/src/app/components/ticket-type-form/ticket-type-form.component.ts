import { ToursService } from './../../services/tours.service';
import { BusinessAccountService } from './../../services/business-account.service';
import { BusinessAccount } from './../../models/business-account';
import { async } from '@angular/core/testing';
import { MessageService } from 'primeng/api';
import { TicketType } from './../../models/ticket-types';
import { SECONDARY_STATUS, TICKET_STATUS, AGENCY_STATUS } from './../../constants/STATUS';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { TicketTypeService } from './../../services/ticket-type.service';
import { Router, ActivatedRoute } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import { Location } from '@angular/common';
import { timer } from 'rxjs';
import { Tour } from '../../models/tours';

@Component({
  selector: 'booking-yacht-ticket-type-form',
  templateUrl: './ticket-type-form.component.html',
  styleUrls: ['./ticket-type-form.component.scss'],
})
export class TicketTypeFormComponent implements OnInit {
  loading = true;
  currentUser!: string;
  form!: FormGroup;
  status = TICKET_STATUS;
  isSubmit = false;
  tickesType?: TicketType;
  business?: BusinessAccount;
  tour!: Tour;
  tourStatus = AGENCY_STATUS;
  constructor(
    private router: Router,
    private location: Location,
    private route: ActivatedRoute,
    private ticketTypeService: TicketTypeService,
    private formBuilder: FormBuilder,
    private messageService: MessageService,
    private bussinessService: BusinessAccountService,
    private tourService: ToursService

  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      if (params.id) {
        this.currentUser = params.id;
        this.ticketTypeService
          .getTicketType(this.currentUser)
          .subscribe(async (res) => {
            this.tickesType = await res.data;
            // console.log(res.data);

            this.bussinessService
              .getBusinessAccountByID(
                res.data.idBusinessTourNavigation.idBusiness
              )
              .subscribe(async (bussinessAccRes) => {
                this.business = await bussinessAccRes.data;
              });
            this.tourService
              .getTour(res.data.idBusinessTourNavigation.idTour)
              .subscribe(async (tourRes) => {
                this.tour = tourRes.data;
                // console.log(this.tour);
              });
            this.ticketTypeForm.status.setValue(res.data.status.toString());
            setTimeout(() => {
              this.loading = false;
            }, 1000);
          });
      } else {
        this.ticketTypeForm.name.setValue('');
        this.ticketTypeForm.price.setValue('');
        this.ticketTypeForm.serviceFeePercentage.setValue('');
        this.ticketTypeForm.status.setValue('');
      }
    });
    this._initForm();
  }

  private _initForm() {
    this.form = this.formBuilder.group({
      // name: ['', [Validators.required]],
      // price: ['', [Validators.required]],
      // serviceFeePercentage: [
      //   '',
      //   [Validators.required, Validators.min(0), Validators.max(10)],
      // ],
      status: ['', [Validators.required]],
    });
  }
  onSubmit() {
    this.isSubmit = true;
    if (!this.currentUser) {
      return;
    } else {
      if (this.form.invalid && this.form.hasValidator) {
        return;
      }
      const ticket: TicketType = {
        id: this.currentUser,
        // name: this.ticketTypeForm.name.value,
        // price: this.ticketTypeForm.price.value,
        status: this.ticketTypeForm.status.value,
      };
      this.ticketTypeService
        .updateTicketType(ticket, this.currentUser)
        .subscribe((res) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Success',
            detail: 'Update Successfull',
          });
          timer(500)
            .toPromise()
            .then(() => {
              this.location.back();
            });
        });
    }
  }
  get ticketTypeForm() {
    return this.form.controls;
  }
  onCancle() {
    this.location.back();
  }
}
