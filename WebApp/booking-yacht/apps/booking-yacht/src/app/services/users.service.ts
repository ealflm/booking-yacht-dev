import { environment } from './../../environments/environment.prod';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  apiURL = environment.apiURL + 'login';
  apiURLAcc = environment.apiURL + 'accounts';
  constructor(private http: HttpClient) {}

  getUsers(): Observable<any> {
    return this.http.get<any>(`${this.apiURL}`);
  }

  signIn(email: string, password: string): Observable<any> {
    return this.http.post(this.apiURL, {
      emailAddress: email,
      password: password,
    });
  }
  handleError(error: HttpErrorResponse) {
    return throwError(error);
  }

  getMyInfor(id: string): Observable<any> {
    return this.http.get(`${this.apiURLAcc}/${id}`);
  }
}
