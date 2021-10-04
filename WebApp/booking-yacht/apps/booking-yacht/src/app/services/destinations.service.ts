import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class DestinationsService {
  apiURL = environment.apiURL + '/api/v1/destinations';
  constructor(private http: HttpClient) {}
  getDestinations(status?: string): Observable<any> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get<any>(this.apiURL, { params: params });
  }
}
