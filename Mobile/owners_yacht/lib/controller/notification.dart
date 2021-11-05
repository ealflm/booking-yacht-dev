import 'package:firebase_messaging/firebase_messaging.dart';
import 'package:flutter_local_notifications/flutter_local_notifications.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/home.dart';

class NotificationController extends GetxController {
  static final FlutterLocalNotificationsPlugin _notificationsPlugin =
      FlutterLocalNotificationsPlugin();
  static void initialize() {
    const InitializationSettings initializationSettings =
        InitializationSettings(
            android: AndroidInitializationSettings('@mipmap/ic_launcher'));
    _notificationsPlugin.initialize(initializationSettings,
        onSelectNotification: (String? route) async {
      if (route != null) {
        print("object ne bn oi" + route);
        // Get.to(OrderScreen(), binding: OrderBinding());
        // Get.to(route);
        HomeController _homeController = Get.find<HomeController>();
        _homeController.changeTabIndex(1);
        // Get.toNamed(route);
      }
    });
  }

  static void display(RemoteMessage message) async {
    try {
      final id = DateTime.now().millisecondsSinceEpoch ~/ 1000;
      const NotificationDetails notificationDetails = NotificationDetails(
        android: AndroidNotificationDetails('booking_yacht', 'booking_yacht channel',
            importance: Importance.max, priority: Priority.high),
      );
      await _notificationsPlugin.show(
        id,
        message.notification!.title,
        message.notification!.title,
        notificationDetails,
        payload: message.data['route'],
      );
    } catch (e) {
      // ignore: avoid_print
      print(e.toString());
    }
  }
}
