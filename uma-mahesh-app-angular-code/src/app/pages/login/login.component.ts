import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Component } from '@angular/core';
import { Route, Router } from '@angular/router';
import { UserService } from 'src/app/services/User/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  registerObj : any ={
    "userName" : "",
    "password": "",
    "email": "",
  }

  loginObj : any = {
    "userName" : "",
    "password": ""
  }

  constructor(private http: HttpClient,
    private user: UserService,
    private route: Router){}

 ngOninIt(): void {

 }

 onRegister(userName,password,email): void{
  debugger
  this.user.userRegistration(userName,password,email)
  .subscribe((res:any) => {
    debugger;
    if(res.status == 200){
      alert('Registration is Successful')
    }
    this.route.navigateByUrl('/login')
  },

  (error: HttpErrorResponse) => {
    debugger
    if(error.error.includes(userName)){
      alert('UserName already taken, Please try another')
    }
})
 }

 onLogin(userName,password): void{
  debugger
  this.user.userLogin(userName,password)
  .subscribe((res:any) => {
    debugger
    if(typeof res === null || typeof res === undefined)
    {
    alert('Login was not successful!');
    }
    else{
    alert('Login successful');
    localStorage.setItem('loginToken',res)
    this.route.navigateByUrl('/nav-bar')
    }

  },
  (error: HttpErrorResponse) => {
    debugger;
    alert(error.error);
    })
 }
}
