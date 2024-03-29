import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class AgenciesService {
  apiURL = environment.apiURL + 'agencies';

  constructor(private http: HttpClient) {}

  getAgencies(status?: string): Observable<any> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get<any>(this.apiURL, { params: params });
  }
  deleteAgency(id: string): Observable<any> {
    return this.http.delete(`${this.apiURL}/${id}`);
  }
  getAgency(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiURL}/${id}`);
  }
  updateStatus(id: string, status: string) {
    return this.http.put(`${this.apiURL}/${id}`, { status: status });
  }
  countAgency(): Observable<any> {
    return this.http.get(`${this.apiURL}/counting`);
  }
}

