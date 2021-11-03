import 'package:get/get.dart';
import 'package:owners_yacht/controller/ticket_type.dart';
import 'package:owners_yacht/controller/tour.dart';
import 'package:owners_yacht/controller/trip.dart';
import '/controller/menu.dart';
import '/controller/home.dart';
import '/controller/login.dart';
import '../controller/qr_code.dart';
import '../controller/order.dart';
import '/controller/yacht.dart';

class Binding implements Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => LoginController());
    Get.lazyPut(() => HomeController());
    Get.lazyPut(() => OrderController());
    Get.lazyPut(() => YachtController());
    Get.lazyPut(() => QRCodeController());
    Get.lazyPut(() => TripController());
    Get.lazyPut(() => TourController());
    Get.lazyPut(() => TicketTypeController());
    Get.lazyPut(() => MenuController());
  }
}
