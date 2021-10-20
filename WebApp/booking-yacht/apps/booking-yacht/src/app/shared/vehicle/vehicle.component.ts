import { VEHICLE_STATUS } from './../../constants/STATUS';
import { timer } from 'rxjs';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AGENCY_STATUS } from '../../constants/STATUS';

@Component({
  selector: 'booking-yacht-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.scss'],
})
export class VehicleComponent implements OnInit {
  @Input() bussinessVehicleChild: any[] = [];
  vehicleStatus = VEHICLE_STATUS;
  @Output() pageing = new EventEmitter<any>();
  loading = true;
  constructor() {
    timer(1000).subscribe(() => {
      this.loading = false;
    });
  }

  ngOnInit(): void {}
  paginate(event: any) {
    // console.log(event);
    const page = event.page;
    const rows = event.rows;
    this.pageing.emit({ page, rows });
  }
}
