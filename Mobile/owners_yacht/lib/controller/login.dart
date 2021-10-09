import 'package:firebase_auth/firebase_auth.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:flutter/material.dart';
import 'package:owners_yacht/controller/home.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:get/get.dart';
import 'package:google_sign_in/google_sign_in.dart';
import '/screens/home.dart';
import '/screens/login.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class LoginController extends GetxController {
  // Intilize the flutter app
  late FirebaseApp firebaseApp;
  late User firebaseUser;
  late FirebaseAuth firebaseAuth = FirebaseAuth.instance;

  Future<void> initlizeFirebaseApp() async {
    firebaseApp = await Firebase.initializeApp();
  }

  Future<void> login() async {
    try {
      // Get.dialog(Center(child: LoadingWidget()), barrierDismissible: false);

      await initlizeFirebaseApp();

      firebaseAuth = FirebaseAuth.instance;

      final googleUser = await GoogleSignIn().signIn();

      final googleAuth = await googleUser!.authentication;

      final credential = GoogleAuthProvider.credential(
        accessToken: googleAuth.accessToken,
        idToken: googleAuth.idToken,
      );

      final userCredentialData =
          await FirebaseAuth.instance.signInWithCredential(credential);
      firebaseUser = userCredentialData.user!;
      var idToken = await firebaseUser.getIdToken();
      Map data = {'idToken': idToken};
      var body = json.encode(data);
      Map<String, String> headers = {"Content-Type": "application/json"};

      var response = await http.post(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/admin/open-login"),
          headers: headers,
          body: body);

      // final responseData = json.decode(response.body);
      // var token = responseData['data'];
      //  var response2 = await http.get(
      // Uri.parse(
      //     "https://booking-yacht.azurewebsites.net/api/v1/agencies"),
      // headers: {"Content-Type": "application/json", "Authorization": "Bearer $token"},
      // );
      // print(response);
      // log(response2.toString());
      update();

      if (response.statusCode == 200) {
        final prefs = await SharedPreferences.getInstance();
        final responseData = json.decode(response.body);
        var token = responseData['data'];
        prefs.setString('token', token);
        Get.to(Home());
      } else {
        Get.snackbar('Sign In Error', 'Error Signing in',
            duration: Duration(seconds: 5),
            backgroundColor: Colors.black,
            colorText: Colors.white,
            snackPosition: SnackPosition.BOTTOM,
            icon: Icon(
              Icons.error,
              color: Colors.red,
            ));
        Get.back();
      }
    } catch (ex) {
      Get.back();
    }
  }

  Future<void> logout() async {
    // final prefs = await SharedPreferences.getInstance();
    // prefs.clear();
    HomeController controller = Get.find<HomeController>();
    controller.tabIndex = 0;
    
    await GoogleSignIn().signOut();
    await FirebaseAuth.instance.signOut();
    Get.to(Login());
  }
}
