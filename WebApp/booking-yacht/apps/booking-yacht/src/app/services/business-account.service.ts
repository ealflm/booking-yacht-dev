import { BusinessAccount } from '../models/business-account';
import { Observable } from 'rxjs';
import { environment } from './../../environments/environment.prod';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root',
})
export class BusinessAccountService {
  private apiURLBusiness = environment.apiURL + 'business-accounts';

  constructor(private http: HttpClient) {}

  getBusinessAccount(status: string): Observable<any> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get<any>(this.apiURLBusiness, { params: params });
  }
  createBusinessAccount(
    businessAccount: BusinessAccount
  ): Observable<BusinessAccount> {
    return this.http.post<BusinessAccount>(
      this.apiURLBusiness,
      businessAccount
    );
  }
  getBusinessAccountByID(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiURLBusiness}/${id}`);
  }
  updateBusinessAccount(
    id: string,
    businessAccount: BusinessAccount
  ): Observable<any> {
    // console.log(businessAccount);
    return this.http.put<BusinessAccount>(
      `${this.apiURLBusiness}/${id}`,
      businessAccount
    );
  }
  deleteBusinessAccount(id: string): Observable<any> {
    return this.http.delete(`${this.apiURLBusiness}/${id}`);
  }
}
