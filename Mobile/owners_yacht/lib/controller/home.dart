import 'package:get/get.dart';
import 'package:owners_yacht/controller/menu.dart';
import 'package:owners_yacht/controller/ticket_type.dart';
import 'package:owners_yacht/controller/transaction.dart';
import 'package:owners_yacht/controller/trip.dart';

class HomeController extends GetxController {
  var tabIndex = 0;
  final TripController _tripController = Get.find<TripController>();
  final TransactionController _transactionController =
      Get.find<TransactionController>();
  final TicketTypeController _ticketTypeController =
      Get.find<TicketTypeController>();
  final MenuController _menuController = Get.find<MenuController>();
  void changeTabIndex(int index) {
    tabIndex = index;
    update();
    switch (index) {
      case 0:
        _transactionController.getTransaction();
        break;
      case 1:
        _tripController.getTrip();
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
