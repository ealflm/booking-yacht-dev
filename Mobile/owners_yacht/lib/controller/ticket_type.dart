import 'dart:convert';

import 'package:get/get.dart';
import 'package:owners_yacht/models/ticket_type.dart';
import 'package:owners_yacht/screens/ticket_modify.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class TicketTypeController extends GetxController {
  var isLoading = true.obs;
  bool isAdding = true;
  String id = "";
  List<TicketType> listTicketType = <TicketType>[].obs;
  var ticketDetail = TicketType();

  Future<List<TicketType>?> getTicketType() async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/trips"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var tickets = ticketReponseFromJson(jsonString);
        if (tickets.data != null) {
          listTicketType = tickets.data as List<TicketType>;
        }

        update();
        Get.to(TicketType());
      } else {
        return null;
      }
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return listTicketType;
  }

  Future<TicketType> getTicketTypeDetail(String id) async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/tours/${id}"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var tour = ticketReponseFromJson(jsonString);
        if (tour.data != null) {
          ticketDetail = tour.data as TicketType;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return ticketDetail;
  }

  void editTicketType(TicketType ticket) async {
    isAdding = false;
    id = ticket.id!;
    Get.to(TicketModify());
  }

  void addTicketType() async {
    isAdding = true;
    Get.to(TicketModify());
  }

  void save() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      if (!isAdding) {
        TicketType ticket = TicketType(
            id: "8c1f568a-b338-4037-8298-0d41c87a76f8",
            name: "Vip",
            price: 100,
            status: 4,
            commissionFeePercentage: 5,
            serviceFeePercentage: 5,
            idBusinessTour: "0fe6ad4f-0ebd-4bd7-9a0c-4bc6387b9e5e");
        String body = json.encode(ticket);

        final response = await http.put(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/ticket-types/${id}"),
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer $token",
          },
          body: body,
        );
        if (response.statusCode == 200) {
          getTicketType();
          update();
          Get.back();
        } else {
          print('loi o save roi ');
        }
      } else {
        TicketType ticket = TicketType(
            name: "Vip",
            price: 100,
            status: 4,
            commissionFeePercentage: 5,
            serviceFeePercentage: 5,
            idBusinessTour: "0fe6ad4f-0ebd-4bd7-9a0c-4bc6387b9e5e");
        String body = json.encode(ticket);
        final response = await http.post(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/ticket-types"),
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer $token",
          },
          body: body,
        );
        if (response.statusCode == 200) {
          getTicketType();
          update();
          Get.back();
        } else {
          print('loi o save roi ');
        }
      }
    } catch (error) {
      print(error);
    }
  }

  void deleteTicketType(String id) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.delete(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/ticket-types/${id}"),
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer $token",
          });
      if (response.statusCode == 200) {
        getTicketType();
        update();
        Get.back();
      } else {
        print('loi o delete');
      }
    } catch (error) {
      print(error);
    }
  }
}
