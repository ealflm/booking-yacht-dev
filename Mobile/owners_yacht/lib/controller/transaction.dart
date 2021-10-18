import 'package:get/get.dart';
import 'package:owners_yacht/models/transaction.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class TransactionController extends GetxController {
  var isLoading = true.obs;
  List<Transaction> listTransaction = <Transaction>[].obs;
  var transactionDetail = Transaction();

  Future<List<Transaction>> getTransaction() async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/orders"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var transactions = transactionReponseFromJson(jsonString);
        if (transactions.data != null) {
          listTransaction = transactions.data as List<Transaction>;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return listTransaction;
  }

  Future<Transaction> getTransactionDetail(String id) async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/orders/${id}"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var transactions = transactionReponseFromJson(jsonString);
        if (transactions.data != null) {
          transactionDetail = transactions.data as Transaction;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return transactionDetail;
  }
}
