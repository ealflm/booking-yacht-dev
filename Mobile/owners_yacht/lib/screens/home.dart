import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/screens/ticket_type.dart';
import '/controller/home.dart';
import 'menu.dart';
import 'trips.dart';
import 'transactions.dart';

class Home extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return GetBuilder<HomeController>(
      builder: (controller) {
        return Scaffold(
          body: IndexedStack(
            index: controller.tabIndex,
            children: [
              Transactions(),
              Trips(),
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
                icon: Icons.timeline_sharp,
                label: 'Chuyến đi',
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
