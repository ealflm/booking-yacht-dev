import { ConfirmationService, MessageService } from 'primeng/api';
import { Apartment } from './../../models/apartments';
import { async } from '@angular/core/testing';
import { ApartmentsService } from './../../services/apartments.service';
import { DestinationsService } from './../../services/destinations.service';
import { Destination } from './../../models/destinations';
import { TOUR_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-destinations',
  templateUrl: './destinations.component.html',
  styleUrls: ['./destinations.component.scss'],
})
export class DestinationsComponent implements OnInit {
  destinations: Destination[] = [];
  destinationStatus = TOUR_STATUS;
  status = [
    { id: '0', lable: 'NONE' },
    { id: '1', lable: 'ACCEPTED' },
    { id: '2', lable: 'REJECT' },
  ];
  loading = true;
  placeType: any;
  constructor(
    private desService: DestinationsService,
    private apartmentService: ApartmentsService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    this.getDestinations();
  }

  private getDestinations() {
    this.desService.getDestinations().subscribe((res) => {
      this.destinations = res.data;
      // console.log(this.destinations);s

      this.destinations.map((destination: Destination) => {
        this.apartmentService
          .getApartment(destination.idPlaceType)
          .subscribe((res) => {
            destination.apartmentName = res.data.name;
            destination.apartmentId = res.data.id;
            setTimeout(() => {
              this.loading = false;
            }, 1000);
          });
      });
    });
  }

  onChangeStatus(id: string) {
    if (id == '0') {
      this.desService.getDestinations().subscribe((res) => {
        this.destinations = res.data;
        console.log(this.destinations);

        this.destinations.map((destination: Destination) => {
          this.apartmentService
            .getApartment(destination.idPlaceType)
            .subscribe((res) => {
              destination.apartmentName = res.data.name;
              destination.apartmentId = res.data.id;
              setTimeout(() => {
                this.loading = false;
              }, 1000);
            });
        });
      });
    } else {
      this.desService.getDestinations(id).subscribe((res) => {
        this.destinations = res.data;
        console.log(this.destinations);

        this.destinations.map((destination: Destination) => {
          this.apartmentService
            .getApartment(destination.idPlaceType)
            .subscribe((res) => {
              destination.apartmentName = res.data.name;
              destination.apartmentId = res.data.id;
              setTimeout(() => {
                this.loading = false;
              }, 1000);
            });
        });
      });
    }
  }
  editDes(id: string) {}
  deleteDes(id: string) {
    this.confirmationService.confirm({
      message: 'Bạn có chắc muốn từ chối tour này ?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.desService.deleteDes(id).subscribe((response) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Successfull',
            detail: 'Update successfull',
          });
          this.getDestinations();
        });
      },
    });
  }
  getValue(event: Event) {
    return (event.target as HTMLInputElement).value;
  }
}
