import 'package:get/get.dart';
import '/controller/menu.dart';
import '/controller/home.dart';
import '/controller/login.dart';
import '../controller/qr_code.dart';
import '../controller/transaction.dart';
import '/controller/verification.dart';
import '/controller/yacht.dart';

class Binding implements Bindings {
  @override
  void dependencies() {
    Get.lazyPut(() => LoginController());
    Get.lazyPut(() => HomeController());
    Get.lazyPut(() => TransactionController());
    Get.lazyPut(() => YachtController());
    Get.lazyPut(() => QRCodeController());
    Get.lazyPut(() => VerificationController());
    Get.lazyPut(() => MenuController());
  }
}
