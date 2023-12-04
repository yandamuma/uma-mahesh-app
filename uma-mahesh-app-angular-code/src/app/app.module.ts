import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContactPageComponent } from './contact-page/contact-page.component';
import { EditListingPageComponent } from './edit-listing-page/edit-listing-page.component';
import { ListingDataFormComponent } from './listing-data-form/listing-data-form.component';
import { ListingDetailPageComponent } from './listing-detail-page/listing-detail-page.component';
import { ListingsPageComponent } from './listings-page/listings-page.component';
import { MyListingsPageComponent } from './my-listings-page/my-listings-page.component';
import { NavBarComponent } from './pages/nav-bar/nav-bar.component';
import { NewListingPageComponent } from './new-listing-page/new-listing-page.component';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RestaurantsComponent } from './Restaurants/restaurants/restaurants.component';
import { EditRestaurantComponent } from './Restaurants/edit-restaurant/edit-restaurant.component';
import { LoginComponent } from './pages/login/login.component';
import { LayoutComponent } from './pages/layout/layout.component';
import { CommonInterceptor } from './services/custom/common.interceptor';

@NgModule({
  declarations: [
    AppComponent,
    ContactPageComponent,
    EditListingPageComponent,
    ListingDataFormComponent,
    ListingDetailPageComponent,
    ListingsPageComponent,
    MyListingsPageComponent,
    NavBarComponent,
    NewListingPageComponent,
    RestaurantsComponent,
    EditRestaurantComponent,
    LoginComponent,
    LayoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [{
    provide: HTTP_INTERCEPTORS,
    useClass: CommonInterceptor,
    multi:true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
