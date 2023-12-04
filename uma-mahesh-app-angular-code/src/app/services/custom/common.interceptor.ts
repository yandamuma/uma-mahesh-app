import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpHeaders
} from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class CommonInterceptor implements HttpInterceptor {

  constructor() {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    const httpHeaders = new HttpHeaders({
      'Access-Control-Allow-Origin': '*'
  });

    const token = localStorage.getItem('loginToken');
    const newCloneRequest = request.clone({
      headers:httpHeaders,
      setHeaders:{
        Authorization: `Bearer ${token}`,
      },

    })
    return next.handle(newCloneRequest);
  }
}
