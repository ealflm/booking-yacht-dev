import 'package:flutter/material.dart';
import 'screens/home.dart';
import 'screens/qr_code.dart';
import 'screens/tour.dart';
import 'screens/verification.dart';
import 'screens/yachts.dart';
import 'binding/bindings.dart';
import 'screens/login.dart';
import 'package:get/get.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  Binding().dependencies();
  runApp(
    GetMaterialApp(
      initialRoute: '/login',
      getPages: [
        GetPage(
          name: '/login',
          page: () => Login(),
          binding: Binding(),
        ),
        GetPage(
          name: '/home',
          page: () => Home(),
          binding: Binding(),
        ),
        GetPage(
          name: '/tour',
          page: () => Tour(),
          binding: Binding(),
        ),
        GetPage(
          name: '/yachts',
          page: () => Yachts(),
          binding: Binding(),
        ),
        GetPage(
          name: '/qr-code',
          page: () => QRScan(),
          binding: Binding(),
        ),
        GetPage(
            name: '/verification',
            page: () => Verification(),
            binding: Binding()),
        GetPage(
          name: '/menu',
          page: () => Verification(),
          binding: Binding(),
        ),
      ],
    ),
  );
}
