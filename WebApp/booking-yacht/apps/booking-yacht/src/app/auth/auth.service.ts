import { MessageService } from 'primeng/api';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { LocalStorageService } from './localstorage.service';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import {
  getAuth,
  signInWithPopup,
  GoogleAuthProvider,
  signInWithRedirect,
} from 'firebase/auth';
import { environment } from '../../environments/environment.prod';
import { GoogleLoginProvider } from 'angularx-social-login';
import { async } from '@angular/core/testing';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  apiURL = environment.apiURL + '/api/v1/admins/open-login';
  userData: any;
  constructor(
    private router: Router,
    private localStorage: LocalStorageService,
    private http: HttpClient,
    private messageService: MessageService
  ) {}

  get isLoggedIn(): boolean {
    const user = JSON.parse(this.localStorage.getToken() || '{}');
    return user !== null && user.emailVerified !== false ? true : false;
  }
  loginWithGoogle(token: any): Observable<any> {
    return this.http.post<{ idToken: string }>(`${this.apiURL}`, {
      idToken: token,
    });
  }
  googleLogin() {
    const provider = new GoogleAuthProvider();
    const auth = getAuth();
    signInWithPopup(auth, provider)
      .then(async (result) => {
        // This gives you a Google Access Token. You can use it to access the Google API.
        const credential = GoogleAuthProvider.credentialFromResult(result);
        const accessToken = credential?.accessToken;
        const idToken = await result.user.getIdToken(true);

        // console.log(idToken);

        // console.log(idToken);

        // console.log(result);

        // The signed-in user info.
        // console.log(result);

        this.loginWithGoogle(idToken).subscribe((res) => {
          // console.log(res.data);
          if (res.data !== undefined) {
            this.localStorage.setToken(res.data);
            this.router.navigate(['dashboard']);
            // console.log(res.data);
          } else {
            this.messageService.add({
              severity: 'error',
              summary: 'Error',
              detail: res.error,
            });
          }

          // this.userData = result.user;
        });
        // console.log(result.user);
        // console.log(credential);

        // console.log(this.userData);

        // ...
      })
      .catch((error) => {
        // Handle Errors here.
        const errorCode = error.code;
        const errorMessage = error.message;
        // The email of the user's account used.
        const email = error.email;
        // The AuthCredential type that was used.
        const credential = GoogleAuthProvider.credentialFromError(error);
        // ...
      });
  }
}
