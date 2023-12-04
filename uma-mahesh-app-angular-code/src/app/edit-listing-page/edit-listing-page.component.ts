import { Component } from '@angular/core';
import { Listing, Products } from '../types';
import { ActivatedRoute, Router } from '@angular/router';
import { fakeMyListings } from '../fake-data';
import { ProductsService } from '../services/Products/products.service';

@Component({
  selector: 'app-edit-listing-page',
  templateUrl: './edit-listing-page.component.html',
  styleUrls: ['./edit-listing-page.component.css']
})
export class EditListingPageComponent {
  product: Products;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private prod: ProductsService
  ) { }

  ngOnInit(): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.prod.getProductById(id)
    .subscribe(response => {
      this.product = response;
    })

  }

  onSubmit({name,color,price,size}): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.prod.editProduct(id,name,color,price,size)
    .subscribe( () =>
    {
    this.router.navigateByUrl('/my-listings');
    });
  }
}
