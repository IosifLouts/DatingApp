import { Injectable } from '@angular/core';
import { NgxSpinnerService } from 'ngx-spinner';

@Injectable({
  providedIn: 'root'
})
export class BusyService {
 busyRequestCount = 0; //increment when a request haopens, decrement when a request completes
  constructor(private spinnerService: NgxSpinnerService) { }

  busy() {
    this.busyRequestCount++;
    this.spinnerService.show(undefined, {
      type: 'line-scale-party', //there are 50 spinner types and we can change them anytime
      bdColor:'rgba(255,255,255,0)',
      color: '#333333'
    }); //that's what we are gonna show when we are busy
  }

  idle() {
    this.busyRequestCount--;
    if(this.busyRequestCount <= 0) {
      this.busyRequestCount = 0;
      this.spinnerService.hide()
    }
  }
}
