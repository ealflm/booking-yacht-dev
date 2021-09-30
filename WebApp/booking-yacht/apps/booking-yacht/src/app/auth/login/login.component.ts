import { MessageService } from 'primeng/api';
import { UsersService } from './../../services/users.service';
import { Router } from '@angular/router';
import { Component, OnInit } from '@angular/core';
import {
  SocialAuthService,
  SocialUser,
  GoogleLoginProvider,
} from 'angularx-social-login';
import { AuthService } from '../auth.service';
import { LocalStorageService } from '../localstorage.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'booking-yacht-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.sass'],
})
export class LoginComponent implements OnInit {
  socialUser?: SocialUser;
  isLoggedin?: boolean;
  response: any;
  userForm!: FormGroup;
  user: any;
  isSubmit = false;
  loading = false;
  constructor(
    private router: Router,
    private localStorageService: LocalStorageService,
    private authService: AuthService,
    private formBuilder: FormBuilder,
    private userService: UsersService,
    private messageService: MessageService
  ) {}

  ngOnInit(): void {
    if (this.localStorageService.getToken()) {
      this.router.navigate(['/dashboard']);
    }
    this._initForm();
  }

  private _initForm() {
    this.userForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
    });
  }
  onSignIn() {
    this.loading = true;
    setTimeout(() => {
      this.loading = false;
    }, 1000);
    this.isSubmit = true;
    this.userService
      .signIn(this.usersForm.email.value, this.usersForm.password.value)
      .subscribe(
        (res) => {
          this.localStorageService.setToken(res);
          this.router.navigate(['dashboard']);
        },
        (error) => {
          this.messageService.add({
            severity: 'error',
            summary: 'email or password incorect',
          });
        }
      );
  }

  loginWithGoogle(): void {
    this.authService.googleLogin();
  }
  logOut(): void {}

  get usersForm() {
    return this.userForm.controls;
  }
}
