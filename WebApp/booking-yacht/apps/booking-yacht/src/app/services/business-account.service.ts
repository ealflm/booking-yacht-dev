import { BusinessAccount } from './../models/businessAcount';
import { Observable } from 'rxjs';
import { environment } from './../../environments/environment.prod';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
@Injectable({
  providedIn: 'root',
})
export class BusinessAccountService {
  private apiURLBusiness = environment.apiURL + 'BusinessAccounts';

  constructor(private http: HttpClient) {}

  getBusinessAccount(): Observable<BusinessAccount[]> {
    return this.http.get<BusinessAccount[]>(this.apiURLBusiness);
  }
  createBusinessAccount(
    businessAccount: BusinessAccount
  ): Observable<BusinessAccount> {
    return this.http.post<BusinessAccount>(
      this.apiURLBusiness,
      businessAccount
    );
  }
  getBusinessAccountByID(id: string): Observable<BusinessAccount> {
    return this.http.get<BusinessAccount>(`${this.apiURLBusiness}/${id}`);
  }
  updateBusinessAccount(
    id: string,
    businessAccount: BusinessAccount
  ): Observable<any> {
    return this.http.put<BusinessAccount>(
      `${this.apiURLBusiness}/${id}`,
      businessAccount
    );
  }
  deleteBusinessAccount(id: string): Observable<any> {
    return this.http.delete(`${this.apiURLBusiness}/${id}`);
  }
}
