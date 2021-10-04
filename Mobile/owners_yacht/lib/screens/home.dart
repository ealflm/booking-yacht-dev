import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:get/get.dart';

import '/controller/home.dart';

import 'menu.dart';
import 'qr-code.dart';
import 'tour.dart';
import 'verification.dart';
import 'yachts.dart';

class Home extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return GetBuilder<HomeController>(
      builder: (controller) {
        return Scaffold(
          body: IndexedStack(
            index: controller.tabIndex,
            children: [
              Yachts(),
              Tour(),
              QRScan(),
              Verification(),
              Menu(),
            ],
          ),
          bottomNavigationBar: BottomNavigationBar(
            items: [
              _bottomNavigationBarItem(
                icon: Icons.home,
                label: 'Home',
              ),
              _bottomNavigationBarItem(
                icon: FontAwesomeIcons.ship,
                label: 'Yacht',
              ),
              _bottomNavigationBarItem(
                icon: Icons.qr_code_2,
                label: 'Scan QR Code',
              ),
              _bottomNavigationBarItem(
                icon: Icons.checklist_rtl,
                label: 'Verification',
              ),
              _bottomNavigationBarItem(
                icon: Icons.menu,
                label: 'Menu',
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
