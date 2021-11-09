import 'package:get/get.dart';
import 'package:flutter/material.dart';
import 'package:owners_yacht/controller/qr_code.dart';
import 'package:owners_yacht/controller/tour.dart';
import 'package:owners_yacht/controller/trip.dart';
import 'package:owners_yacht/controller/yacht.dart';
import 'package:owners_yacht/screens/qr_code.dart';
import 'package:owners_yacht/widgets/nav_bar.dart';
import '/controller/login.dart';
import 'package:settings_ui/settings_ui.dart';

class Menu extends StatelessWidget {
  final LoginController loginController = Get.find<LoginController>();
  final YachtController yachtController = Get.find<YachtController>();
  // final QRCodeController qrCodeController = Get.find<QRCodeController>();
  final TourController tourController = Get.find<TourController>();
  final TripController tripController = Get.find<TripController>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const NavBar(
        title: 'Thêm',
        automaticallyImplyLeading: false,
      ),
      body: Padding(
        padding: const EdgeInsets.only(top: 4.0),
        child: SettingsList(
          sections: [
            SettingsSection(
              title: 'Tàu',
              tiles: [
                SettingsTile(
                  title: 'Quản lý tàu',
                  leading: const Icon(Icons.directions_boat_sharp),
                  onPressed: (BuildContext context) =>
                      yachtController.fetchYachts(),
                ),
              ],
            ),
            SettingsSection(
              title: 'Mã QR',
              tiles: [
                SettingsTile(
                  title: 'Quét mã QR',
                  leading: const Icon(Icons.qr_code_2_outlined),
                  onPressed: (BuildContext context) => Get.to(QRScan()),
                ),
              ],
            ),
            SettingsSection(
              title: 'Tour',
              tiles: [
                SettingsTile(
                  title: 'Tour',
                  leading: const Icon(Icons.tour_outlined),
                  onPressed: (BuildContext context) => tourController.getTour(),
                ),
              ],
            ),
            SettingsSection(
              title: 'Tài khoản',
              tiles: [
                SettingsTile(
                  title: 'Đăng xuất',
                  leading: const Icon(Icons.logout),
                  onPressed: (BuildContext context) => loginController.logout(),
                ),
              ],
            ),
          ],
        ),
      ),
    );
  }
}
