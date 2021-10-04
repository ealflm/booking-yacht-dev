// import 'package:firebase_auth/firebase_auth.dart';
// import 'package:flutter/material.dart';
// import 'package:google_sign_in/google_sign_in.dart';
import 'package:get/get.dart';

class LoginController extends GetxController{}



// class GoogleSignInProvider extends ChangeNotifier {
//   final googleSignIn = GoogleSignIn();

//   GoogleSignInAccount? _user;

//   GoogleSignInAccount get user => user;

//   Future googleLogin() async {
//     final googleUser = await googleSignIn.signIn();

//     if(googleUser != null){
//        _user = googleUser;
//     } else {
//       return;
//     }

//     final googleAuth = await googleUser.authentication;
//     final credential = GoogleAuthProvider.credential(
//       accessToken: googleAuth.accessToken,
//       idToken: googleAuth.idToken,
//     );
//     await FirebaseAuth.instance.signInWithCredential(credential);
//     notifyListeners();
//   }
// }
