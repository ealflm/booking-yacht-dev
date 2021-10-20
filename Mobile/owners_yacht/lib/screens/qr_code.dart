import 'package:owners_yacht/widgets/nav_bar.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import '../controller/qr_code.dart';

class QRScan extends StatelessWidget {

  final QRCodeController qrCodeController = Get.find<QRCodeController>();
  @override
  Widget build(BuildContext context) {
    return SafeArea(
      child: Scaffold(
        appBar: const NavBar(
          title: 'Quét QR Code',
          automaticallyImplyLeading: true,
        ),
        body: Stack(
          alignment: Alignment.center,
          children: <Widget>[
            QRView(
              key: qrCodeController.qrKey,
              onQRViewCreated: qrCodeController.onQRViewCreated,
              overlay: QrScannerOverlayShape(
                borderColor: Theme.of(context).accentColor,
                borderRadius: 10,
                borderLength: 20,
                borderWidth: 10,
                cutOutSize: MediaQuery.of(context).size.width * 0.8,
              ),
            ),
            Positioned(
              bottom: 10,
              child: Container(
                padding: const EdgeInsets.all(12),
                decoration: BoxDecoration(
                  borderRadius: BorderRadius.circular(8),
                  color: Colors.white24,
                ),
                child: Text(
                  qrCodeController.barcode != null
                      ? 'Result: ${qrCodeController.barcode!.code}'
                      : 'Quét QR Code',
                  maxLines: 3,
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
