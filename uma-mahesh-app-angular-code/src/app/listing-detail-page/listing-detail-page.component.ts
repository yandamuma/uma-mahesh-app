import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
 import { fakeListings } from '../fake-data';
import { Listing, Products } from '../types';
import { ProductsService } from '../services/Products/products.service';
import { Params } from '@angular/router';

@Component({
  selector: 'app-listing-detail-page',
  templateUrl: './listing-detail-page.component.html',
  styleUrls: ['./listing-detail-page.component.css']
})
export class ListingDetailPageComponent implements OnInit {
  product: Products ;



  constructor(
    private route: ActivatedRoute,
    private prod: ProductsService
  ) { }

  ngOnInit(): void {

    //let id:number = +this.route.snapshot.paramMap.get('id');


    //  this.listing = fakeListings.find(listing => listing.id === id);
    //this.prod.getProductById(id)


    const id = +this.route.snapshot.paramMap.get('id');
    // this.listing = fakeListings.find(listing => listing.id === id);

    this.prod.getProductById(id)
    .subscribe({
      next:(response) => {
        this.product = response;
      }
    }
    );

  }

}
