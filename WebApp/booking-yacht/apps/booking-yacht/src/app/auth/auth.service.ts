import { LocalStorageService } from './localstorage.service';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { getAuth, signInWithPopup, GoogleAuthProvider } from 'firebase/auth';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  userData: any;
  constructor(
    private router: Router,
    private localStorage: LocalStorageService
  ) {}

  get isLoggedIn(): boolean {
    const user = JSON.parse(this.localStorage.getToken() || '{}');
    return user !== null && user.emailVerified !== false ? true : false;
  }

  googleLogin() {
    const provider = new GoogleAuthProvider();
    const auth = getAuth();
    signInWithPopup(auth, provider)
      .then((result) => {
        // This gives you a Google Access Token. You can use it to access the Google API.
        const credential = GoogleAuthProvider.credentialFromResult(result);
        const token = credential?.accessToken;
        // The signed-in user info.
        this.localStorage.setToken(token);
        this.userData = result.user;
        // console.log(this.userData);
        this.router.navigate(['dashboard']);

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
