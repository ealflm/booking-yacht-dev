import 'package:owners_yacht/controller/ticket.dart';
import 'package:owners_yacht/widgets/nav_bar.dart';
import 'package:qr_code_scanner/qr_code_scanner.dart';
import 'package:flutter/material.dart';
import 'package:get/get.dart';
import '../controller/qr_code.dart';
import 'dart:developer';
import 'dart:io';

class QRScan extends StatefulWidget {
  QRScan({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _QRScanState();
}

class _QRScanState extends State<QRScan> {
  TicketController _ticketController = Get.find<TicketController>();
  Barcode? result;
  QRViewController? controller;
  final GlobalKey qrKey = GlobalKey(debugLabel: 'QR');

  // In order to get hot reload to work we need to pause the camera if the platform
  // is android, or resume the camera if the platform is iOS.
  @override
  void reassemble() {
    super.reassemble();
    if (Platform.isAndroid) {
      controller!.pauseCamera();
    }
    controller!.resumeCamera();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const NavBar(
        title: 'Quét QR Code',
        automaticallyImplyLeading: true,
      ),
      body: Stack(
        alignment: Alignment.center,
        children: <Widget>[
          _buildQrView(context),
          Positioned(
            bottom: 10,
            child: Container(
              padding: const EdgeInsets.all(12),
              decoration: BoxDecoration(
                borderRadius: BorderRadius.circular(8),
                color: Colors.white24,
              ),
              child: Text(
                result != null ? 'Quét xong' : 'Quét QR Code',
                maxLines: 3,
              ),
            ),
          ),
        ],
      ),

      // Expanded(
      //   flex: 1,
      //   child: FittedBox(
      //     fit: BoxFit.contain,
      //     child: Column(
      //       // mainAxisAlignment: MainAxisAlignment.spaceEvenly,
      //       children: <Widget>[
      //         if (result != null)
      //           Text(
      //               'Barcode Type: ${result!.format}   Data: ${result!.code}')
      // else
      //   const Text('Scan a code'),
      // Row(
      //   mainAxisAlignment: MainAxisAlignment.center,
      //   crossAxisAlignment: CrossAxisAlignment.center,
      //   children: <Widget>[],
      // ),
      // Row(
      //   mainAxisAlignment: MainAxisAlignment.center,
      //   crossAxisAlignment: CrossAxisAlignment.center,
      //   children: <Widget>[
      //     Container(
      //       margin: const EdgeInsets.all(8),
      //       child: ElevatedButton(
      //         onPressed: () async {
      //           await controller?.pauseCamera();
      //         },
      //         child: const Text('pause',
      //             style: TextStyle(fontSize: 20)),
      //       ),
      //     ),
      //     Container(
      //       margin: const EdgeInsets.all(8),
      //       child: ElevatedButton(
      //         onPressed: () async {
      //           await controller?.resumeCamera();
      //         },
      //         child: const Text('resume',
      //             style: TextStyle(fontSize: 20)),
      //       ),
      //     )
      //   ],
      // ),
      //       ],
      //     ),
      //   ),
      // )
      // ],
    );
  }

  Widget _buildQrView(BuildContext context) {
    // For this example we check how width or tall the device is and change the scanArea and overlay accordingly.
    var scanArea = (MediaQuery.of(context).size.width < 400 ||
            MediaQuery.of(context).size.height < 400)
        ? 150.0
        : 300.0;
    // To ensure the Scanner view is properly sizes after rotation
    // we need to listen for Flutter SizeChanged notification and update controller
    return QRView(
      key: qrKey,
      onQRViewCreated: _onQRViewCreated,
      overlay: QrScannerOverlayShape(
          borderColor: Colors.red,
          borderRadius: 10,
          borderLength: 30,
          borderWidth: 10,
          cutOutSize: scanArea),
      onPermissionSet: (ctrl, p) => _onPermissionSet(context, ctrl, p),
    );
  }

  void _onQRViewCreated(QRViewController controller) {
    setState(() {
      this.controller = controller;
    });
    controller.scannedDataStream.listen((scanData) {
      setState(() {
        result = scanData;
        _ticketController.checkTicket(scanData.code);
      });
    });
  }

  void _onPermissionSet(BuildContext context, QRViewController ctrl, bool p) {
    if (!p) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('no Permission')),
      );
    }
  }

  @override
  void dispose() {
    controller?.dispose();
    super.dispose();
  }
}
// class QRScan extends StatelessWidget {
//   final QRCodeController qrCodeController = Get.find<QRCodeController>();
//   @override
//   Widget build(BuildContext context) {
//     return SafeArea(
//       child: Scaffold(
//         appBar: const NavBar(
//           title: 'Quét QR Code',
//           automaticallyImplyLeading: true,
//         ),
//         body: Stack(
//           alignment: Alignment.center,
//           children: <Widget>[
//             QRView(
//               key: qrCodeController.qrKey,
//               onQRViewCreated: qrCodeController.onQRViewCreated,
//               overlay: QrScannerOverlayShape(
//                 borderColor: Theme.of(context).accentColor,
//                 borderRadius: 10,
//                 borderLength: 20,
//                 borderWidth: 10,
//                 cutOutSize: MediaQuery.of(context).size.width * 0.8,
//               ),
//             ),
//             Positioned(
//               bottom: 10,
//               child: Container(
//                 padding: const EdgeInsets.all(12),
//                 decoration: BoxDecoration(
//                   borderRadius: BorderRadius.circular(8),
//                   color: Colors.white24,
//                 ),
//                 child: Text(
//                   qrCodeController.barcode != null
//                       ? 'Result: ${qrCodeController.barcode!.format} - ${qrCodeController.barcode!.code}'
//                       : 'Quét QR Code',
//                   maxLines: 3,
//                 ),
//               ),
//             ),
//           ],
//         ),
//       ),
//     );
//   }
// }
