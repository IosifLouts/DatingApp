import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { User } from './_models/user';
import { AccountService } from './_services/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dating app!';
  users : any;
  

  constructor(private accountService: AccountService) {

  }

  ngOnInit() {
    this.setCurrentUser();
  }
 
  //We wanna take a look inside our browser local storage and see if we got a key or an object with the key of user
  setCurrentUser(){
    const user: User = JSON.parse(localStorage.getItem('user')); //we use parse to take out the object of its stringified form
    this.accountService.setCurrrentUser(user);
  }

  

}
