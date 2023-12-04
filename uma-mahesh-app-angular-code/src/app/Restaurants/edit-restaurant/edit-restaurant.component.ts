import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { RestaurantService } from '../services/restaurant.service';
import { Restaurants } from '../Interfaces/restaurants';

@Component({
  selector: 'app-edit-restaurant',
  templateUrl: './edit-restaurant.component.html',
  styleUrls: ['./edit-restaurant.component.css']
})
export class EditRestaurantComponent implements OnInit {

  form: FormGroup;
  restaurant: Restaurants;

  name: string = '';
  cuisine: number = 0;
  address: string = '';
  constructor(
    private formBuilder: FormBuilder,
    private router: Router,
    private route: ActivatedRoute,
    private rest:RestaurantService
  ){}

  ngOnInit(): void {
    // this.businessForm.controls.proof.patchValue(this.defaultValue);
    this.form = this.formBuilder.group({
      name: this.formBuilder.control(''),
      cuisine: this.formBuilder.control(''),
      address: this.formBuilder.control(''),
    });

    const id = +this.route.snapshot.paramMap.get('id');
    this.rest.getRestaurantById(id)
    .subscribe(response => {
      this.restaurant = response,
      this.name =this.restaurant.Name,
      this.cuisine = this.restaurant.Cuisine,
      this.address=this.restaurant.Address })


    }





  onSubmit({name,cuisine,address}){
    const id = +this.route.snapshot.paramMap.get('id');
    this.rest.editRestaurant(id , name , cuisine ,address)
    .subscribe( () =>
      this.router.navigateByUrl('/restaurants')
    )
  }



}
