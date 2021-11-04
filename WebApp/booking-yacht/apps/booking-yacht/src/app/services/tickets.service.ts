import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class TicketsService {
  apiURL = environment.apiURL + 'tickets';
  constructor(private http: HttpClient) {}
  getTickets(id?: string): Observable<any> {
    let params = new HttpParams();
    if (id) {
      params = params.append('id-order', id);
    }
    return this.http.get(this.apiURL, { params: params });
  }

  getTicket(id: string): Observable<any> {
    // let params = new HttpParams();
    // if (id) {
    //   params = params.append('id-order', id);
    // }
    return this.http.get(`${this.apiURL}/${id}`);
  }
  countTicket(): Observable<any> {
    return this.http.get(`${this.apiURL}/counting`);
  }
}
