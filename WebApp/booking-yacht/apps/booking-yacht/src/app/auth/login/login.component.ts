import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import {
  SocialAuthService,
  SocialUser,
  GoogleLoginProvider,
} from 'angularx-social-login';
import { AuthService } from '../auth.service';
import { LocalStorageService } from '../localstorage.service';

@Component({
  selector: 'booking-yacht-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass'],
})
export class LoginComponent implements OnInit {
  socialUser?: SocialUser;
  isLoggedin?: boolean;
  response: any;
  constructor(
    private router: Router,
    private localStorageService: LocalStorageService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    if (this.localStorageService.getToken()) {
      this.router.navigate(['/dashboard']);
    }
  }
  loginWithGoogle(): void {
    this.authService.googleLogin();
  }
  logOut(): void {}
}
