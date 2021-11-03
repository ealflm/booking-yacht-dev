import 'package:get/get.dart';
import 'package:owners_yacht/controller/menu.dart';
import 'package:owners_yacht/controller/ticket_type.dart';
import 'package:owners_yacht/controller/order.dart';
import 'package:owners_yacht/controller/trip.dart';

class HomeController extends GetxController {
  var tabIndex = 0;
  final TripController _tripController = Get.find<TripController>();
  final OrderController _orderController =
      Get.find<OrderController>();
  final TicketTypeController _ticketTypeController =
      Get.find<TicketTypeController>();
  final MenuController _menuController = Get.find<MenuController>();
  void changeTabIndex(int index) {
    tabIndex = index;
    update();
    switch (index) {
      case 0:
        _tripController.getBusinessTour();
        break;
      case 1:
        _orderController.getOrder();
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
