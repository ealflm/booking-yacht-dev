import { Tour } from './../models/tours';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class ToursService {
  apiURL = environment.apiURL + '/api/v1/tours';
  constructor(private http: HttpClient) {}

  getTours(status: string): Observable<any> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get<any>(`${this.apiURL}`, { params: params });
  }

  getTour(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiURL}/${id}`);
  }

  createTour(tour: Tour): Observable<any> {
    return this.http.post<Tour>(`${this.apiURL}`, tour);
  }

  updateTour(tour: Tour, id: string): Observable<any> {
    return this.http.put<Tour>(`${this.apiURL}/${id}`, tour);
  }
  deteleTour(id: string): Observable<any> {
    return this.http.delete(`${this.apiURL}/${id}`);
  }
}
