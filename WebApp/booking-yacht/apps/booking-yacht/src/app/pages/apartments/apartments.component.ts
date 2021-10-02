import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { MessageService, ConfirmationService } from 'primeng/api';
import { ApartmentsService } from './../../services/apartments.service';
import { Apartment } from './../../models/apartment_models';
import { APARTMENT_STATUS } from './../../constants/BUSINESS_STATUS';
import { Component, OnInit } from '@angular/core';
import { ThrowStmt } from '@angular/compiler';
import { pipe } from 'rxjs';

@Component({
  selector: 'booking-yacht-apartments',
  templateUrl: './apartments.component.html',
  styleUrls: ['./apartments.component.scss'],
})
export class ApartmentsComponent implements OnInit {
  apartments: Apartment[] = [];
  apartmentStatus = APARTMENT_STATUS;
  loading = true;
  status: any[] = [];
  constructor(
    private apartmentService: ApartmentsService,
    private confirmationService: ConfirmationService,
    private router: Router,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.getAparments();
    this._mapApartmentStatus();
  }

  getAparments() {
    this.apartmentService.getApartments().subscribe((res) => {
      this.apartments = res.data;
      setTimeout(() => {
        this.loading = false;
      }, 1000);
    });
  }
  _mapApartmentStatus() {
    this.status = Object.keys(APARTMENT_STATUS).map((key) => {
      {
        return {
          id: key,
          name: APARTMENT_STATUS[key].lable,
        };
      }
    });
  }

  deleteApartment(id: string) {
    this.confirmationService.confirm({
      message: 'Are you sure want to disable Apartment ?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.apartmentService.deleteApartment(id).subscribe(() => {
          this.messageService.add({
            severity: 'success',
            summary: 'Successfull',
            detail: 'Update successfull',
          });
          this.getAparments();
        });
      },
    });
  }
  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }
  editApartment(id: string) {
    this.router.navigate([`apartments/form/${id}`]);
  }
  newApartment() {
    this.router.navigate(['apartments/form']);
  }
}
