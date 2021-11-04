import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';

@Injectable({
  providedIn: 'root',
})
export class PaymentService {
  apiURL = environment.apiURL + '/vn-pay';

  constructor(private http: HttpClient) {}
  getPayment(ip: string, amount: number, id: string) {
    let params = new HttpParams();
    if (ip && id && amount) {
      params = params.append('ip', ip);
      params = params.append('id-business', id);
      params = params.append('amount', amount);
    }
    return this.http.get(`${this.apiURL}`, { params: params });
  }
}
