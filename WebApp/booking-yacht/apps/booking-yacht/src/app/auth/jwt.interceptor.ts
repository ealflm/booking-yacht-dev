import { environment } from './../../environments/environment.prod';
import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { LocalStorageService } from './localstorage.service';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
  constructor(private localstorageService: LocalStorageService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    const token = this.localstorageService.getToken();
    const isAPIUrl = request.url.startsWith(environment.apiURL);
    if (token && isAPIUrl) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${token}`,
        },
      });
    }
    return next.handle(request);
  }
}
