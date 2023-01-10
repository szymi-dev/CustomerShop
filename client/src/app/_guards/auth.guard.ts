import { Injectable } from '@angular/core';
import { CanActivate, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AccountService } from '../_services/account.service';
import  { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor (private accountService: AccountService){}
  canActivate(): Observable<boolean | UrlTree>{
    return this.accountService.currentUser$.pipe(
      map(user => {
        if(user) return true;
        else{
          console.log("You shall not pass");
          return false;
        }
      })
    )
  }
  
  
}
