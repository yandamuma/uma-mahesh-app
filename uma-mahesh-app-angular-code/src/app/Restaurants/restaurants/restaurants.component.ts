import { Component, OnInit } from '@angular/core';
import { RestaurantService } from '../services/restaurant.service';
import { Restaurants } from '../Interfaces/restaurants';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-restaurants',
  templateUrl: './restaurants.component.html',
  styleUrls: ['./restaurants.component.css']
})
export class RestaurantsComponent implements OnInit {

  restaurants: Restaurants[]
  constructor(
    private rest:RestaurantService
  ){}
  ngOnInit(): void {


    this.rest.getAllRestaurants()
      .subscribe(response =>
        this.restaurants = response)
    }

}


