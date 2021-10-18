import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
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
}
