import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.css']
})
export class NavBarComponent implements OnInit {

  constructor(private route:Router){}
  ngOnInit(): void {

  }

onSignOut(): void{
  if(confirm("Are you sure to sign out ?")){
    this.route.navigateByUrl("/login")
  }


}}
