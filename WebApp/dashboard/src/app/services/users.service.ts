import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UsersService {

  private basePath = 'https://booking-yacht-dev.southeastasia.cloudapp.azure.com/api/member';
  constructor(private http: HttpClient) {}
  
  public getUsers(): Observable<any> {
    return this.http.get(this.basePath);
  }
}
