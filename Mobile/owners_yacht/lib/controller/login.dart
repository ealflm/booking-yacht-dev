import 'package:firebase_auth/firebase_auth.dart';
import 'package:firebase_core/firebase_core.dart';
import 'package:owners_yacht/controller/home.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:get/get.dart';
import 'package:google_sign_in/google_sign_in.dart';
import '/screens/home.dart';
import '/screens/login.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:jwt_decode/jwt_decode.dart';

class LoginController extends GetxController {
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
      var idToken;
      firebaseAuth = FirebaseAuth.instance;

      User? currentUser = FirebaseAuth.instance.currentUser;
      if (currentUser == null) {
        final googleUser = await GoogleSignIn().signIn();

        final googleAuth = await googleUser!.authentication;

        final credential = GoogleAuthProvider.credential(
          accessToken: googleAuth.accessToken,
          idToken: googleAuth.idToken,
        );
        final userCredentialData =
            await FirebaseAuth.instance.signInWithCredential(credential);
        firebaseUser = userCredentialData.user!;
        idToken = await firebaseUser.getIdToken();
      } else {
        idToken = await currentUser.getIdToken();
      }

      Map data = {'idToken': idToken};
      var body = json.encode(data);
      Map<String, String> headers = {"Content-Type": "application/json"};
      var response = await http.post(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/open-login"),
          headers: headers,
          body: body);
      if (response.statusCode == 200) {
        final prefs = await SharedPreferences.getInstance();
        final responseData = json.decode(response.body);
        Map<String, dynamic> payload = Jwt.parseJwt(response.body);
        print(payload);
        var token = responseData['data'];
        prefs.setString('token', token);
        prefs.setString('idBusiness', payload['Id']);
        Get.to(Home());
      } else {
        Get.back();
      }
    } catch (ex) {
      Get.back();
    }
  }

  Future<void> logout() async {
    final prefs = await SharedPreferences.getInstance();
    prefs.clear();
    HomeController controller = Get.find<HomeController>();
    controller.tabIndex = 0;

    await GoogleSignIn().signOut();
    await FirebaseAuth.instance.signOut();
    Get.to(Login());
  }

  void sendToken(String tokenDevice) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    // final String? idBusiness = prefs.getString('idBusiness');
    try {
      String body = json.encode(
          {'id': '68554b5a-817b-453c-992c-149662a8e710', 'token': tokenDevice});

      final response = await http.post(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/registrationtoken"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token",
        },
        body: body,
      );
      if (response.statusCode == 200) {
        print('ok device len');
        print(tokenDevice);
      } else {
        print('loi o save roi ');
      }
    } catch (e) {}
  }
}
