import 'package:flutter/material.dart';
import '../models/yacht.dart';
import '../widgets/app-drawer.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

import 'qr-code.dart';
import 'tour.dart';
import 'verification.dart';
import 'yachts.dart';
import 'yacht-manager.dart';

class Home extends StatefulWidget {
  @override
  State<Home> createState() => _HomeState();
}

class _HomeState extends State<Home> {
  int selectedIndex = 0;
  final List<Widget> screens = [
    YachtGrid(),
    Tour(),
    QRScan(),
    Verification(),
  ];
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      drawer: AppDrawer(),
      bottomNavigationBar: BottomNavigationBar(
        currentIndex: selectedIndex,
        onTap: (index) => setState(() => selectedIndex = index),
        items: [
          BottomNavigationBarItem(icon: Icon(Icons.home), label: 'Home'),
          BottomNavigationBarItem(
              icon: FaIcon(FontAwesomeIcons.ship, size: 20), label: 'Yacht'),
          BottomNavigationBarItem(
              icon: Icon(
                Icons.qr_code_2,
              ),
              label: 'Scan QR Code'),
          BottomNavigationBarItem(
              icon: Icon(Icons.checklist_rtl), label: 'Verification'),
        ],

        backgroundColor: Colors.white,
        selectedItemColor: Colors.black,
        unselectedItemColor: Colors.grey,
        // showUnselectedLabels: false,
      ),
      body: IndexedStack(
        index: selectedIndex,
        children: screens,
      ),
    );
  }
}
