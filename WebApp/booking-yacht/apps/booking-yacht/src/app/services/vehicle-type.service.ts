import { VehicleType } from './../models/vehicle-types';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class VehicleTypeService {
  apiURL = environment.apiURL + 'vehicle-types';
  constructor(private http: HttpClient) {}
  getVehicleTypes(status?: string): Observable<any> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get(`${this.apiURL}`, { params: params });
  }
  getVehicleType(id: string): Observable<any> {
    return this.http.get(`${this.apiURL}/${id}`);
  }
  deleteVehicle(id: string): Observable<any> {
    return this.http.delete(`${this.apiURL}/${id}`);
  }
  createVehicle(vehicleType: VehicleType): Observable<any> {
    return this.http.post<VehicleType>(`${this.apiURL}`, vehicleType);
  }
  updateVehicle(vehicleType: VehicleType, id: string): Observable<any> {
    return this.http.put<VehicleType>(`${this.apiURL}/${id}`, vehicleType);
  }

  getVehicle(id: string): Observable<any> {
    return this.http.get(
      `https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/${id}`
    );
  }
}
