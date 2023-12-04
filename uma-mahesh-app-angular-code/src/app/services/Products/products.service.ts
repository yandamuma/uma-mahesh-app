
import { Injectable } from '@angular/core';
import { HttpClient ,HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { Listing, Products } from '../../types';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
  export class ProductsService {

    baseApiUrl = environment.baseApiUrl

    constructor(private http: HttpClient) { }

    getAllProducts(): Observable<Products[]> {
      return this.http.get<Products[]>(this.baseApiUrl+'/api/Products')
    }
    getAllProductsByPage(page: number): Observable<Products[]> {
      return this.http.get<Products[]>(this.baseApiUrl+'/api/Products?Page='+ page)
    }

  getAllListings(): Observable<Listing[]> {
    return this.http.get<Listing[]>(this.baseApiUrl+'/api/Products')
  }

  getProductById(id: number): Observable<Products>
  {
  return this.http.get<Products>(this.baseApiUrl+'/api/Products/' + id)
  }

  getProductsByColor(color: string): Observable<Products[]>
  {
    return this.http.get<Products[]>(this.baseApiUrl+'/api/Products/' + color)
  }

  getProductsByListPrice(): Observable<Products[]>{
    return this.http.get<Products[]>(this.baseApiUrl+'/api/prod/')
  }

  createProduct(id: number,name: string, color: string,
               price: number, size: string): Observable<Products>
  {
    return this.http.post<Products>
      (this.baseApiUrl+'/api/Products/' ,
        {name,color,price,size} )
  }

  editProduct(id: number,name: string, color: string,
    price: number, size: string): Observable<Products>
  {
    return this.http.put<Products>
      (this.baseApiUrl+'/api/Products/'+ id ,
        {name,color,price,size} )
  }

  deleteProduct(id: number): Observable<Products>
  {
    return this.http.delete<Products>(this.baseApiUrl+'/api/Products/' + id)
  }

}
