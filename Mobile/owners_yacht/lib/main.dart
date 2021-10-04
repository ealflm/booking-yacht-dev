import 'package:flutter/material.dart';
import 'package:owners_yacht/controller/home.dart';
import 'package:owners_yacht/screens/home.dart';
import 'package:owners_yacht/screens/qr-code.dart';
import 'package:owners_yacht/screens/tour-detail.dart';
import 'package:owners_yacht/screens/tour.dart';
import 'package:owners_yacht/screens/verification.dart';
import 'package:owners_yacht/screens/yachts.dart';
import 'binding/bindings.dart';
import 'screens/login.dart';

import 'package:get/get.dart';

void main() {
  runApp(GetMaterialApp(
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
  ));
}
