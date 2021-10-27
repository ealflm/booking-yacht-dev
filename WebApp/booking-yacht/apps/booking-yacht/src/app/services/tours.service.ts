import { Tour } from './../models/tours';
import { Observable, throwError } from 'rxjs';
import {
  HttpClient,
  HttpErrorResponse,
  HttpParams,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.prod';
import { catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root',
})
export class ToursService {
  apiURL = environment.apiURL + 'tours';
  constructor(private http: HttpClient) {}

  getTours(status: string): Observable<any> {
    let params = new HttpParams();
    if (status) {
      params = params.append('status', status);
    }
    return this.http.get<any>(`${this.apiURL}`, { params: params });
  }

  getTour(id: string): Observable<any> {
    return this.http.get<any>(`${this.apiURL}/${id}`);
  }

  createTour(tour: Tour): Observable<any> {
    return this.http.post<Tour>(`${this.apiURL}`, tour);
  }

  updateTour(tour: Tour, id: string): Observable<any> {
    return this.http.put<Tour>(`${this.apiURL}/${id}`, tour);
  }
  deteleTour(id: string): Observable<any> {
    return this.http.delete(`${this.apiURL}/${id}`);
  }
  uploadTourImage(image: FormData): Observable<any> {
    return this.http
      .post(`${this.apiURL}/upload`, image, {
        reportProgress: true,
        observe: 'events',
      })
      .pipe(catchError(this.errorMgmt));
  }
  errorMgmt(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Get client-side error
      errorMessage = error.error.message;
    } else {
      // Get server-side error
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    console.log(errorMessage);
    return throwError(errorMessage);
  }
}
