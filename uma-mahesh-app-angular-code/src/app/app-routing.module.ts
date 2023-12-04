import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListingsPageComponent } from './listings-page/listings-page.component';
import { ListingDetailPageComponent } from './listing-detail-page/listing-detail-page.component';
import { ContactPageComponent } from './contact-page/contact-page.component';
import { EditListingPageComponent } from './edit-listing-page/edit-listing-page.component';
import { MyListingsPageComponent } from './my-listings-page/my-listings-page.component';
import { NewListingPageComponent } from './new-listing-page/new-listing-page.component';
import { RestaurantsComponent } from './Restaurants/restaurants/restaurants.component';
import { EditRestaurantComponent } from './Restaurants/edit-restaurant/edit-restaurant.component';
import { LoginComponent } from './pages/login/login.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { NavBarComponent } from './pages/nav-bar/nav-bar.component';

const routes: Routes = [

  {path:'', component:LoginComponent, pathMatch:'full'},
  {path:'login', component: LoginComponent},

  {path:'nav-bar',component:NavBarComponent,children:[
    {path:'',component:LayoutComponent},
  ]},

  {path:'',component:NavBarComponent,children:[
  {path:'listings' , component:ListingsPageComponent , pathMatch:'full'},
  {path:'listings/:id' , component:ListingDetailPageComponent},
  {path:'contact/:id' , component:ContactPageComponent},
  {path:'edit-listing/:id', component: EditListingPageComponent },
  {path:'my-listings', component: MyListingsPageComponent },
  {path:'new-listing', component: NewListingPageComponent },
  {path:'restaurants' , component: RestaurantsComponent},
  {path:'edit-restaurant/:id', component: EditRestaurantComponent},
  ]},

  {path:'**',component:LoginComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
