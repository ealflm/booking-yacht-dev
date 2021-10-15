import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class VehicleService {
  apiURL = environment.apiURL + 'vehicles';
  constructor(private http: HttpClient) {}

  getVehiclesByBussiness(idBussiness: string): Observable<any> {
    let params = new HttpParams();
    if (idBussiness) {
      params = params.append('id-business', idBussiness);
    }
    return this.http.get(`${this.apiURL}`, { params: params });
  }
}
