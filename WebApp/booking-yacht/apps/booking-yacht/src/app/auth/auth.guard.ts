import { SocialUser, SocialAuthService } from 'angularx-social-login';
// import { LocalstorageService, LocalStorageService } from './localstorage.service';
import { Observable } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot,
  CanActivate,
  Router,
  RouterStateSnapshot,
  UrlTree,
} from '@angular/router';
import { LocalStorageService } from './localstorage.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuardService implements CanActivate {
  constructor(
    private router: Router,
    private localStorage: LocalStorageService
  ) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): Observable<boolean> | boolean {
    const token = this.localStorage.getToken();
    if (token) {
      // const tokenDecode = JSON.parse(atob(token.split('.')[1]));
      // console.log(tokenDecode);

      return true;
    } else {
      this.router.navigate(['login']);
      return false;
    }

    // return this.socialAuthService.authState.pipe(
    //   map((socialUser: SocialUser) => !!socialUser),
    //   tap((isLoggedIn: boolean) => {
    //     if (!isLoggedIn) {
    //       this.router.navigate(['login']);
    //     }
    //   })
    // );
  }
}
