import { Apartment } from './../models/apartment_models';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class ApartmentsService {
  apiURL = environment.apiURL + '/api/v1/place-types';
  constructor(private http: HttpClient) {}
  getApartments(): Observable<any> {
    return this.http.get<any>(`${this.apiURL}`);
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
