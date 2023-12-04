import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export default class EmailService {

  baseApiUrl = environment.baseApiUrl


  constructor(private http: HttpClient) { }

  sendMessage(toemail: string , subject: string ,  body: string  ) : Observable<any>{
    return this.http.post<any>(this.baseApiUrl + '/api/Email/' ,
                              {toemail,subject,body})
  }

}
