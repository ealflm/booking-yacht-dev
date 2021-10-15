import { Component, Input, OnInit } from '@angular/core';
import { AGENCY_STATUS } from '../../constants/STATUS';

@Component({
  selector: 'booking-yacht-vehicle',
  templateUrl: './vehicle.component.html',
  styleUrls: ['./vehicle.component.scss'],
})
export class VehicleComponent implements OnInit {
  @Input() bussinessVehicleChild: any[] = [];
  vehicleStatus = AGENCY_STATUS;
  constructor() {}

  ngOnInit(): void {}
}
