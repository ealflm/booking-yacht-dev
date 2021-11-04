import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:owners_yacht/screens/menu.dart';
import 'package:owners_yacht/screens/tours.dart';
import 'screens/ticket_type.dart';
import 'package:owners_yacht/screens/trips.dart';
import 'screens/home.dart';
import 'screens/qr_code.dart';
import 'screens/trips.dart';
import 'screens/order.dart';
import 'screens/yachts.dart';
import 'binding/bindings.dart';
import 'screens/login.dart';
import 'package:get/get.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:firebase_core/firebase_core.dart';

void main() async {
  WidgetsFlutterBinding.ensureInitialized();
  initializeDateFormatting();
  await Firebase.initializeApp();
  Binding().dependencies();
  runApp(
    GetMaterialApp(
      debugShowCheckedModeBanner: false,
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
          name: '/order',
          page: () => Orders(),
          binding: Binding(),
        ),
        GetPage(
          name: '/tours',
          page: () => Tours(),
          binding: Binding(),
        ),
        GetPage(
          name: '/ticket_type',
          page: () => TicketType(),
          binding: Binding(),
        ),
        GetPage(
          name: '/trip',
          page: () => Trips(),
          binding: Binding(),
        ),
        GetPage(
          name: '/menu',
          page: () => Menu(),
          binding: Binding(),
        ),
      ],
    ),
  );
}
