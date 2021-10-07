import { AGENCY_STATUS } from './../../constants/STATUS';
import { map } from 'rxjs/operators';
import { Router } from '@angular/router';
import { MessageService, ConfirmationService } from 'primeng/api';
import { ApartmentsService } from './../../services/apartments.service';
import { Apartment } from '../../models/apartments';
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
  apartmentStatus = AGENCY_STATUS;
  loading = true;
  status = [
    { id: '0', lable: 'None' },
    { id: '1', lable: 'Accepted' },
    { id: '2', lable: 'Reject' },
  ];
  constructor(
    private apartmentService: ApartmentsService,
    private confirmationService: ConfirmationService,
    private router: Router,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.getAparments();
    // this._mapApartmentStatus();
  }

  getAparments() {
    this.apartmentService.getApartments('0').subscribe((res) => {
      setTimeout(() => {
        this.loading = false;
      }, 1000);
      this.apartments = res.data;
    });
  }
  onChangeStatus(id: string) {
    this.loading = true;
    this.apartmentService.getApartments(id).subscribe((res) => {
      setTimeout(() => {
        this.loading = false;
      }, 1000);
      this.apartments = res.data;
    });
  }
  // _mapApartmentStatus() {
  //   this.status = Object.keys(APARTMENT_STATUS).map((key) => {
  //     {
  //       return {
  //         id: key,
  //         name: APARTMENT_STATUS[key].lable,
  //       };
  //     }
  //   });
  // }

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
