export interface Listing {
    // id: number,
    id: string,
    name: string,
    // color: string,
    description: string,
    // size: string,
    // listprice: number,
    price: number,
}

export interface Products{
    id: string,
    name: string,
    color: string,
    size: string,
    price: number,
}

export interface Restaurants{
  id: string,
  name: string,
  cuisine: string,

}

export interface User{
  id: string,
  userName: string,
  password: string,
  email: string,
  token: string

}
