import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';
import { take } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) {} //We store our token as part of our current user, inside our account service
  //When we login we got our currentUser in our account service, because we set that particular property, and our current user is an observable.
  //So we need to get the currentUser outside of that observable.
  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser: User = {} as User;
    //pipe method is used in an observable to chain multiple operators together.
    //We are gonna take and we are gonna take 1 from this observable.
    //With take 1, we only take 1 curentUser and the we complete. In that way we wont have to unsubscribe.
    //We need to subscribe to get what inside this observable.
    this.accountService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user) ; //We set our current user from the account service to this currentUser variable.
    
    if(currentUser) { // we want to clone this request and add our authentication header on to it
      request = request.clone({
        setHeaders : {
          Authorization: `Bearer ${currentUser.token}` //attach our token for every request when we are logged in
        }
      })
    }


    return next.handle(request);
  }
}
