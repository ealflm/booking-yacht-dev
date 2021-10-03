import { ConfirmationService, MessageService } from 'primeng/api';
import { ActivatedRoute, Router } from '@angular/router';
import { ToursService } from './../../services/tours.service';
import { TOUR_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-tours',
  templateUrl: './tours.component.html',
  styleUrls: ['./tours.component.scss'],
})
export class ToursComponent implements OnInit {
  tours: [] = [];
  status: any[] = [];
  loading?: boolean = true;
  tourStatus = TOUR_STATUS;
  constructor(
    private tourService: ToursService,
    private router: Router,
    private confirmationService: ConfirmationService,
    private messageService: MessageService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getTours();
    this._mapTourStatus();
    setTimeout(() => {
      this.loading = false;
    }, 1000);
  }
  private getTours() {
    this.tourService.getTours().subscribe((res) => {
      this.tours = res.data;
    });
  }
  _mapTourStatus() {
    this.status = Object.keys(TOUR_STATUS).map((key) => {
      {
        return {
          id: key,
          name: TOUR_STATUS[key].lable,
        };
      }
    });
  }
  editTour(id: string) {
    this.router.navigate([`tours/form/${id}`]);
  }
  deleteTour(id: string) {
    this.confirmationService.confirm({
      message: 'Are you sure want to disable user ?',
      header: 'Confirmation',
      icon: 'pi pi-exclamation-triangle',
      accept: () => {
        this.tourService.deteleTour(id).subscribe((response) => {
          this.messageService.add({
            severity: 'success',
            summary: 'Successfull',
            detail: 'Update successfull',
          });
          this.getTours();
        });
      },
    });
  }
  newTour() {
    this.router.navigate(['tours/form']);
  }
  getValue(event: Event): string {
    return (event.target as HTMLInputElement).value;
  }
}
