import { HttpClient } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Restaurants } from '../../types';

@Injectable({
  providedIn: 'root'
})
export class RestaurantsService  {
  baseApiUrl = environment.baseApiUrl
  constructor(
    private http: HttpClient
  ) { }


    getAllRestaurants(): Observable<Restaurants[]>
    {
     return this.http.get<Restaurants[]>(this.baseApiUrl + '/api/restaurants')
    }

}
