import { Component, OnInit } from '@angular/core';
import { Listing, Products } from '../types';
 import { fakeListings } from '../fake-data';
import { ProductsService } from '../services/Products/products.service';

@Component({
  selector: 'app-listings-page',
  templateUrl: './listings-page.component.html',
  styleUrls: ['./listings-page.component.css']
})

export class ListingsPageComponent implements OnInit {
  products: Products[] = [];
  numbers: Number[] = [];

  constructor(private prod:ProductsService){
    this.numbers = Array(10).fill(10).map((x,i)=>i);
  }

  public onChange(event): void {  // event will give you full breif of action
    const newVal = event.target.value;
    this.prod.getAllProductsByPage(newVal)
             .subscribe(
             response => {
             this.products = response;
                      });
  }

  ngOnInit(): void {
    this.prod.getAllProducts()
             .subscribe(
             response => {
             this.products = response;
                      });
  }



    //this.listings = fakeListings;
    // this.prod.getAllProducts().subscribe(listings => {
    //   this.listings=listings;
    // });


}
