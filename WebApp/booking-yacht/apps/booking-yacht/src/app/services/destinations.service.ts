import { Desti, Destination } from './../models/destinations';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class DestinationsService {
  apiURL = environment.apiURL + 'destinations';
  constructor(private http: HttpClient) {}
  getDestinations(status?: string): Observable<any> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get<any>(this.apiURL, { params: params });
  }
  deleteDes(id: string): Observable<any> {
    return this.http.delete(`${this.apiURL}/${id}`);
  }
  getDes(id: string): Observable<Destination> {
    return this.http.get<Destination>(`${this.apiURL}/${id}`);
  }

  createDes(des: Desti): Observable<any> {
    return this.http.post<Desti>(`${this.apiURL}`, des);
  }
  updateDes(des: Desti, id: string): Observable<any> {
    return this.http.put<Desti>(`${this.apiURL}/${id}`, des);
  }
}
