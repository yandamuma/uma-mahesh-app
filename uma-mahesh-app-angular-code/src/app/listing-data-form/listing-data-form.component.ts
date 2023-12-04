import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {  Products } from '../types';

@Component({
  selector: 'app-listing-data-form',
  templateUrl: './listing-data-form.component.html',
  styleUrls: ['./listing-data-form.component.css']
})
export class ListingDataFormComponent implements OnInit {

  @Input() buttonText;
  @Input() currentName: string;
  @Input() currentPrice: string;
  @Input() currentColor: string;
  @Input() currentSize: string;

  name: string = '';
  color: string = '';
  price: string = '';
  size: string = '';

@Output() onSubmit = new EventEmitter<Products>();
  constructor(){}

  ngOnInit(): void {
    this.name= this.currentName,
    this.color=this.currentColor,
    this.price=this.currentPrice,
    this.size= this.currentSize
  }

  onButtonClicked(): void{
    this.onSubmit.emit({
      id: null,
      name: this.name,
      color: this.color,
      price: Number(this.price),
      size: this.size
    });
  }

}
