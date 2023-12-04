import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { User } from '../../types';
@Injectable({
  providedIn: 'root'
})


export class UserService {

  baseApiUrl = environment.baseApiUrl
  constructor(private http: HttpClient) {}

  userRegistration(userName: string,password: string,
     email: string): Observable<User>{
      debugger
      const httpOptions : Object = {
        observe: 'response'
     };
      return this.http.post<User>
      (this.baseApiUrl+ '/api/User/register/',{userName,password,email}, httpOptions)
  }

  userLogin(userName: string,password: string,
    ): Observable<any>{
      const httpOptions : Object = {
        responseType: 'text'
      };
     return this.http.post<User>
     (this.baseApiUrl+ '/api/User/login/',{userName,password}, httpOptions)
 }


}


