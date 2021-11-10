// import 'package:flutter/material.dart';
// import 'package:get/get.dart';
// import 'package:owners_yacht/screens/qr_code.dart';
// import 'package:qr_code_scanner/qr_code_scanner.dart';

// class QRCodeController extends GetxController {
//   final qrKey = GlobalKey(debugLabel: 'QR');
//   Barcode? barcode;
//   QRViewController? controller;

//   @override
//   void dispose() {
//     controller?.dispose();
//     super.dispose();
//   }

//   void onQRViewCreated(QRViewController controller) {
//     this.controller = controller;
//     controller.scannedDataStream.listen((event) {
//       barcode = event;
//       update();
//     });
//   }

//   void scanQRCode() {
//     Get.to(QRScan());
//   }
// }
