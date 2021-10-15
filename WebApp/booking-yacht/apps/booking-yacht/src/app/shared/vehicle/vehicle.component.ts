import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { AGENCY_STATUS } from '../../constants/STATUS';

@Component({
  selector: 'booking-yacht-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.scss'],
})
export class VehicleComponent implements OnInit {
  @Input() bussinessVehicleChild: any[] = [];
  vehicleStatus = AGENCY_STATUS;
  @Output() pageing = new EventEmitter<any>();
  constructor() {}

  ngOnInit(): void {}
  paginate(event: any) {
    // console.log(event);
    const page = event.page;
    const rows = event.rows;
    this.pageing.emit({ page, rows });
  }
}
