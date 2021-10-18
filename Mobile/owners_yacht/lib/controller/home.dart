import 'package:get/get.dart';
import 'package:owners_yacht/controller/menu.dart';
import 'package:owners_yacht/controller/ticket_type.dart';
import 'package:owners_yacht/controller/trip.dart';

import 'package:owners_yacht/controller/verification.dart';

class HomeController extends GetxController {
  var tabIndex = 0;
  final TripController _tripController = Get.find<TripController>();
  final VerificationController _verificationController =
      Get.find<VerificationController>();
  final TicketTypeController _ticketTypeController =
      Get.find<TicketTypeController>();
  final MenuController _menuController = Get.find<MenuController>();
  void changeTabIndex(int index) {
    tabIndex = index;
    update();
    switch (index) {
      case 0:
        _tripController.getTrip();
        break;
      case 1:
        _verificationController;
        break;
      case 2:
        _ticketTypeController.getTicketType();
        break;
      case 3:
        _menuController;
        break;
      default:
    }
  }
}
