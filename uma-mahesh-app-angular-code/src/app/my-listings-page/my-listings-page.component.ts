import { Component, OnInit } from '@angular/core';
import { Listing, Products, Restaurants } from '../types';
import { ProductsService } from '../services/Products/products.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-my-listings-page',
  templateUrl: './my-listings-page.component.html',
  styleUrls: ['./my-listings-page.component.css']
})
export class MyListingsPageComponent implements OnInit {

   listings: Products[] = [];

  constructor(
    private prod: ProductsService,
    private route: ActivatedRoute,
    private router: Router
  ){}

  color: string = "string"


  ngOnInit(): void {

    // this.prod.getProductsByColor(this.color)
    // .subscribe(response =>
    //   {
    //     this.listings= response;
    //   })

      this.prod.getProductsByListPrice()
    .subscribe(response => {
      this.listings= response;
    })
  }


  onDeleteClick(listingId: string):void{
    if (confirm (`Are you sure you want to permanantly Delete this item ? `))
    {
      this.prod.deleteProduct(+listingId)
    .subscribe( () =>
    {
      this.listings = this.listings.filter(
        listing => listing.id != listingId
      );
    });
    } else {
      this.router.navigateByUrl('/my-listings')
    }
  }


}
