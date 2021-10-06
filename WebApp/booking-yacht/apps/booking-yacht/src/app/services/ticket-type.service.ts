import { TicketType } from './../models/ticket-types';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class TicketTypeService {
  apiURL = environment.apiURL + '/api/v1/vehicle-types';
  constructor(private http: HttpClient) {}

  getTicketTypes(status?: string): Observable<any> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get(this.apiURL, { params: params });
  }
  getTicketType(id: string): Observable<any> {
    return this.http.get(`${this.apiURL}/${id}`);
  }
  updateTicketType(ticketType: TicketType, id: string): Observable<any> {
    return this.http.put<TicketType>(`${this.apiURL}/${id}`, ticketType);
  }
}
