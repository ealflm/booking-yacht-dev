import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/notification.dart';
import 'package:owners_yacht/screens/ticket_type.dart';
import '/controller/home.dart';
import 'menu.dart';
import 'trips.dart';
import 'order.dart';

class Home extends StatefulWidget {
  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  final HomeController _homeController = Get.find<HomeController>();
  @override
  void initState() {
    super.initState();

    FirebaseMessaging.instance.getInitialMessage().then((message) {
      if (message != null) {
        final routeFromMessage = message.data['route'];
        // Get.to(OrderScreen(), binding: OrderBinding());
        // print(routeFromMessage + 'Hlsssslo');
        _homeController.changeTabIndex(2);
        var _fcm = FirebaseMessaging.instance;
        _fcm.getToken().then((value) => print('The |||' + value!));
      }
    });
    FirebaseMessaging.onMessage.listen((message) {
      if (message.notification != null) {
        print(message.notification!.body);
        print(message.notification!.title);
        NotificationController.display(message);
      }
      var _fcm = FirebaseMessaging.instance;
      _fcm.getToken().then((value) => print('The |||' + value!));
    });

    FirebaseMessaging.onMessageOpenedApp.listen((message) {
      final routeFromMessage = message.data['route'];
      // Get.to(OrderScreen(), binding: OrderBinding());
      // Get.to(routeFromMessage);
      print(routeFromMessage + 'Hllo');
      _homeController.changeTabIndex(2);
      // Get.toNamed(routeFromMessage);
      var _fcm = FirebaseMessaging.instance;
      _fcm.getToken().then((value) => print('The|||' + value!));
    });
  }

  @override
  Widget build(BuildContext context) {
    return GetBuilder<HomeController>(
      builder: (controller) {
        return Scaffold(
          body: IndexedStack(
            index: controller.tabIndex,
            children: [
              Trips(),
              Orders(),
              TicketType(),
              Menu(),
            ],
          ),
          bottomNavigationBar: BottomNavigationBar(
            items: [
              _bottomNavigationBarItem(
                icon: Icons.home,
                label: 'Trang chủ',
              ),
              _bottomNavigationBarItem(
                icon: Icons.backpack_outlined,
                label: 'Đơn đặt tàu',
              ),
              _bottomNavigationBarItem(
                icon: Icons.airplane_ticket_outlined,
                label: 'Loại Vé',
              ),
              _bottomNavigationBarItem(
                icon: Icons.view_list_rounded,
                label: 'Thêm',
              ),
            ],
            backgroundColor: Colors.white,
            selectedItemColor: Colors.black,
            unselectedItemColor: Colors.grey,
            currentIndex: controller.tabIndex,
            onTap: controller.changeTabIndex,
          ),
        );
      },
    );
  }

  _bottomNavigationBarItem({required IconData icon, required String label}) {
    return BottomNavigationBarItem(
      icon: Icon(icon),
      label: label,
    );
  }
}
