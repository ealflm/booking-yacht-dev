import { Router } from '@angular/router';
import { MessageService } from 'primeng/api';
import { VehicleTypeService } from './../../services/vehicle-type.service';
import { STATUS, TOUR_STATUS, AGENCY_STATUS } from './../../constants/STATUS';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'booking-yacht-vehicle-type',
  templateUrl: './vehicle-type.component.html',
  styleUrls: ['./vehicle-type.component.scss'],
})
export class VehicleTypeComponent implements OnInit {
  loading = true;
  vehicleType: any[] = [];
  status = [
    { id: '', lable: 'NONE' },
    { id: '1', lable: 'ENABLE' },
    { id: '2', lable: 'DISABLE' },
  ];
  vehicleTypeStatus = AGENCY_STATUS;
  constructor(
    private vehicleTypeService: VehicleTypeService,
    private messageService: MessageService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.getVehicles();
  }
  getVehicles() {
    this.vehicleTypeService.getVehicleTypes().subscribe((vehicleTyperes) => {
      this.vehicleType = vehicleTyperes.data;
      setTimeout(() => {
        this.loading = false;
      }, 1000);
    });
  }
  onChangeStatus(status?: string) {
    if (!status) {
      this.vehicleTypeService.getVehicleTypes().subscribe((vehicleTyperes) => {
        this.vehicleType = vehicleTyperes.data;
        setTimeout(() => {
          this.loading = false;
        }, 1000);
      });
    } else {
      this.vehicleTypeService
        .getVehicleTypes(status)
        .subscribe((vehicleTyperes) => {
          this.vehicleType = vehicleTyperes.data;
          setTimeout(() => {
            this.loading = false;
          }, 1000);
        });
    }
  }
  editVehicleType(id: string) {
    this.router.navigate([`vehicle-type/form/${id}`]);
  }
  deleteVehicleType(id: string) {
    this.vehicleTypeService.deleteVehicle(id).subscribe(
      (res) => {
        this.messageService.add({
          severity: 'success',
          summary: 'Success',
          detail: 'Disable success full',
        });
      },
      (error) => {
        console.log(error);
      }
    );
  }
  newVehicleType() {
    this.router.navigate([`vehicle-type/form`]);
  }
  getValue(event: Event) {
    return (event.target as HTMLInputElement).value;
  }
}
