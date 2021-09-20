import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class UsersService {
  //apiURL = environment.apiURL;
  apiURL = 'https://booking-yacht-dev.eastasia.cloudapp.azure.com/api/member';
  constructor(private http: HttpClient) {}

  getUsers(): Observable<any> {
    return this.http.get<any>(`${this.apiURL}`);
  }
}
