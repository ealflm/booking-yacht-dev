import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/models/order.dart';
import 'package:owners_yacht/screens/order.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class OrderController extends GetxController {
  var isLoading = true.obs;
  List<Order> listOrders = <Order>[].obs;
  var ordersDetail = Order();

  @override
  onInit() {
    getOrder();
    super.onInit();
  }

  Future<List<Order>> getOrder() async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    final String? idBusiness = prefs.getString('idBusiness');
    try {
      Map<String, String> queryParams = {
        'idBusiness': idBusiness!,
      };

      final response = await http.get(
        Uri.parse(
                "https://booking-yacht.azurewebsites.net/api/v1.0/business/orders")
            .replace(queryParameters: queryParams),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var orders = orderReponseFromJson(jsonString);
        if (orders.data != null) {
          //print(transactions);
          listOrders = orders.data as List<Order>;
        }
        update();
        // Get.to(Transactions());
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return [];
  }

  Future<Order> getTransactionDetail(String id) async {
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
        var orders = orderReponseFromJson(response.body);
        print('hihi');
        // if (transactions.data != null) {
        //   transactionDetail = transactions.data as Transaction;
        // }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return ordersDetail;
  }

  void deleteOrder(String id) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.delete(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/orders/${id}"),
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer $token",
          });
      if (response.statusCode == 200) {
        Get.back();
        Fluttertoast.showToast(
            msg: "Từ chối thành công",
            toastLength: Toast.LENGTH_SHORT,
            gravity: ToastGravity.BOTTOM,
            timeInSecForIosWeb: 1,
            backgroundColor: Colors.white,
            textColor: Colors.black,
            fontSize: 16.0);
        getOrder();
      } else {
        print('loi o delete');
      }
    } catch (error) {
      print(error);
    }
  }
}
