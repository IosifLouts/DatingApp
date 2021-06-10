import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ReplaySubject } from 'rxjs';
import {map} from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root'
})
export class AccountService {

  baseUrl = environment.apiUrl;
  //create an observable to store our user in
  //First create a private property, and we are gonna set it to a special type of observable
  private currenUserSource = new ReplaySubject<User>(1); //create an observable to store our user
  currentUser$ = this.currenUserSource.asObservable(); 
  
  constructor(private http : HttpClient) { }

  login(model: any) {
     return this.http.post(this.baseUrl + 'account/login', model).pipe(
       map((response:User) => {
         const user = response;
         if (user){
           localStorage.setItem('user', JSON.stringify(user)); //populate our user object that we get back inside local storage in the browser. 'user' is the key.
           this.currenUserSource.next(user); //we set the current user that we get back from our API to our observable
         }
       })
     )
  }

  register(model:any) {
   return this.http.post(this.baseUrl +'account/register', model).pipe(
      map((user: User) => {
        if(user) {
          localStorage.setItem('user', JSON.stringify(user));
          this.currenUserSource.next(user);
        }
      })
    )
  }

  setCurrrentUser(user: User) {
              this.currenUserSource.next(user);
  }

  logout() {
    localStorage.removeItem('user');
    this.currenUserSource.next(null);
  }
}
