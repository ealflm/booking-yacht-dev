import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class TicketsService {
  apiURL = environment.apiURL + 'ticket';
  constructor(private http: HttpClient) {}
  getTickets(status?: string): Observable<any> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get(this.apiURL, { params: params });
  }

  getTicket(id: string): Observable<any> {
    return this.http.get(`${this.apiURL}/${id}`);
  }
}
