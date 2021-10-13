import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import 'package:get/get.dart';

import '/controller/home.dart';

import 'menu.dart';
import 'qr_code.dart';
import 'tour_detail.dart';
import 'transaction.dart';
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
              Transaction(),
              TourDetail(),
              Verification(),
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
                icon: Icons.tour_outlined,
                label: 'Tour',
              ),
              _bottomNavigationBarItem(
                icon: Icons.checklist_rtl,
                label: 'Yêu cầu',
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
