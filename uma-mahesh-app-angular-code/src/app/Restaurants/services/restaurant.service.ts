import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Restaurants } from '../Interfaces/restaurants';

@Injectable({
  providedIn: 'root'
})
export class RestaurantService {

  baseApiUrl = environment.baseApiUrl ;

  constructor(
    private http:HttpClient,
    private router:Router,
    private route:ActivatedRoute
  ) { }

  getAllRestaurants() : Observable<Restaurants[]>{
    return this.http.get<Restaurants[]>(this.baseApiUrl+'/api/Restaurants')
  }


  getRestaurantById(id : number) : Observable<Restaurants>{
    return this.http.get<Restaurants>(this.baseApiUrl+'/api/Restaurants/' + id)
  }

  editRestaurant(id:number , name:string ,
     cuisine: number, address: string): Observable<Restaurants>{
    return this.http.put<Restaurants>(
      this.baseApiUrl + '/api/Restaurants/'+ id ,
      {name,cuisine,address}
    )
  }

}
