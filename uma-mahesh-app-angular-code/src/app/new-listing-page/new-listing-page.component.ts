import { Component } from '@angular/core';
import { ActivatedRoute, Router, RouterOutlet } from '@angular/router';
import { ProductsService } from '../services/Products/products.service';

@Component({
  selector: 'app-new-listing-page',
  templateUrl: './new-listing-page.component.html',
  styleUrls: ['./new-listing-page.component.css']
})
export class NewListingPageComponent {

  constructor(private router:Router,
    private route: ActivatedRoute,
    private prod: ProductsService){}

  // onSubmit(){
  //   alert('your new listed is created successfully')
  //   this.router.navigateByUrl('/my-listings')
  // }

  onSubmit({name,color,price,size}): void {
    const id = +this.route.snapshot.paramMap.get('id');
    this.prod.createProduct(id,name,color,price,size)
    .subscribe( () =>
    {
    this.router.navigateByUrl('/my-listings');
    });
  }
}
