import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class OrdersService {
  apiURL = environment.apiURL + 'orders';
  constructor(private http: HttpClient) {}

  getAllOrders(): Observable<any> {
    return this.http.get(this.apiURL);
  }
  getCustomerRecent(amountItem?: number): Observable<any> {
    let params = new HttpParams();
    if (amountItem) {
      params = params.append('amount-item', amountItem);
      params = params.append('page', 1);
    }
    return this.http.get(this.apiURL, { params: params });
  }
  countOrder(): Observable<any> {
    return this.http.get(`${this.apiURL}/counting`);
  }
}
