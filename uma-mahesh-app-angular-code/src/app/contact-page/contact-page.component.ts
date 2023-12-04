import { Component , EventEmitter, OnInit, Output } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Listing, Products } from '../types';
import { Router } from '@angular/router';
import { ProductsService } from '../services/Products/products.service';
import EmailService from '../services/Email/email.service';

@Component({
  selector: 'app-contact-page',
  templateUrl: './contact-page.component.html',
  styleUrls: ['./contact-page.component.css']
})
export class ContactPageComponent implements OnInit {

  product: Products;

constructor(private route:ActivatedRoute,
  private router:Router,
  private prod:ProductsService,
  private email: EmailService){}

  toEmail: string = '';
  message: string = '';
  name: string = '';

  @Output() onSend = new EventEmitter<any>();

ngOnInit(): void {

const id = +this.route.snapshot.paramMap.get('id');

    this.prod.getProductById(id)
    .subscribe({
      next:(response) => {
        this.product = response;
        this.message = `Hi, I'm interested in your ${this.product.name.toLowerCase()}!`;
      }
    });

}


onSendMessage(): void {
  this.onSend.emit({
    toEmail: this.toEmail
  });
  this.email.sendMessage(this.toEmail , this.product.name , this.message)
  .subscribe(response => {
    if(typeof response === null || typeof response === undefined)
    {
    alert('Your message has not been sent!');
    }
    else{
    alert('Your message has been sent!');
    }
    this.router.navigateByUrl('/listings');
  });

}




}
