import { Apartment } from '../models/apartments';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class ApartmentsService {
  apiURL = environment.apiURL + '/api/v1/place-types';
  constructor(private http: HttpClient) {}
  getApartments(status: string): Observable<any> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get<any>(this.apiURL, { params: params });
  }
  getApartment(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiURL}/${id}`);
  }
  createApartment(apartment: Apartment): Observable<any> {
    return this.http.post(`${this.apiURL}`, apartment);
  }

  updateApartment(apartment: Apartment, id: string): Observable<any> {
    return this.http.put<Apartment>(`${this.apiURL}/${id}`, apartment);
  }

  deleteApartment(id: string): Observable<any> {
    return this.http.delete(`${this.apiURL}/${id}`);
  }
}
