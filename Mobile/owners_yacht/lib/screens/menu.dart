import 'package:get/get.dart';
import 'package:flutter/material.dart';
import '/controller/login.dart';

class Menu extends StatelessWidget {
  final LoginController controller = Get.find<LoginController>();
  @override
  Widget build(BuildContext context) {
    return Center(
      child: ElevatedButton(
        onPressed: () {
          controller.logout();
        },
        child: Text('Logout'),
      ),
    );
  }
}
