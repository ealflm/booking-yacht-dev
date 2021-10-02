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
import { HttpErrorResponse } from '@angular/common/http';

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
    if (this.userForm.invalid) {
      return;
    }

    this.userService
      .signIn(this.usersForm.email.value, this.usersForm.password.value)
      .subscribe(
        (res) => {
          if (res.data !== undefined) {
            this.localStorageService.setToken(res.data);
            this.router.navigate(['dashboard']);
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: res.error,
            });
          }
        },
        (error: HttpErrorResponse) => {
          console.log(error);
          if (error.status === 400) {
            this.messageService.add({
              severity: 'error',
              summary: 'email or password incorect',
              detail: error.message,
            });
          }
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
