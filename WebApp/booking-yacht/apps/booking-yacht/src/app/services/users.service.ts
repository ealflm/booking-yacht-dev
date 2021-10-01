import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  apiURL = environment.apiURL + 'Admins/login';

  constructor(private http: HttpClient) {}

  getUsers(): Observable<any> {
    return this.http.get<any>(`${this.apiURL}`);
  }

  signIn(email: string, password: string): Observable<any> {
    return this.http.post(
      this.apiURL,
      {
        emailAddress: email,
        password: password,
        token: 'string',
      },
      { responseType: 'text' }
    );
  }
  handleError(error: HttpErrorResponse) {
    return throwError(error);
  }
}
