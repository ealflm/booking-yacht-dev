import 'dart:convert';

import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/models/ticket_type.dart';
import 'package:owners_yacht/screens/ticket_type_modify.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class TicketTypeController extends GetxController {
  final GlobalKey<FormState> ticketTypeFormKey = GlobalKey<FormState>();

  var isLoading = true.obs;
  bool isAdding = true;
  List<TicketType> listTicketType = <TicketType>[].obs;
  var ticketDetail = TicketType();
  var selectedValue;

  String id = "";
  TextEditingController nameController = TextEditingController();
  TextEditingController priceController = TextEditingController();
  TextEditingController statusController = TextEditingController();
  TextEditingController commissionFeeController = TextEditingController();
  TextEditingController serviceFeeController = TextEditingController();
  TextEditingController idTourController = TextEditingController();

  void onSelected(String value) {
    selectedValue = value;
    idTourController.text = value;
    update();
  }

  Future<List<TicketType>?> getTicketType() async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/ticket-types"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var tickets = ticketTypeReponseFromJson(jsonString);
        if (tickets.data != null) {
          listTicketType = tickets.data as List<TicketType>;
        }
        update();
        // Get.to(TicketType());
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
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/ticket-types/${id}"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var tickets = ticketTypeReponseFromJson(response.body);
        if (tickets.data != null) {
          ticketDetail = tickets.data as TicketType;
        }
        update();
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
    selectedValue = ticket.idTour;
    nameController.text = ticket.name!;
    priceController.text = ticket.price.toString();
    statusController.text = ticket.status.toString();
    commissionFeeController.text = ticket.commissionFeePercentage.toString();
    serviceFeeController.text = ticket.serviceFeePercentage.toString();
    idTourController.text = ticket.idBusinessTour.toString();

    Get.to(TicketTypeModify());
  }

  void addTicketType() async {
    isAdding = true;
    nameController.clear();
    priceController.clear();
    statusController.clear();
    commissionFeeController.clear();
    serviceFeeController.clear();
    idTourController.clear();
    Get.to(TicketTypeModify());
  }

  void save() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    final isValid = ticketTypeFormKey.currentState!.validate();
    if (!isValid) {
      return;
    }
    ticketTypeFormKey.currentState!.save();
    try {
      if (!isAdding) {
        TicketType ticket = TicketType(
            id: id,
            name: nameController.text,
            price: double.parse(priceController.text),
            status: int.parse(statusController.text),
            commissionFeePercentage: double.parse(commissionFeeController.text),
            serviceFeePercentage: double.parse(serviceFeeController.text),
            idTour: idTourController.text,
            idBusiness: '68554b5a-817b-453c-992c-149662a8e710');
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
        String body = json.encode({
          'name': nameController.text,
          'price': double.parse(priceController.text),
          'status': 3,
          'commissionFeePercentage': double.parse(commissionFeeController.text),
          'serviceFeePercentage': double.parse(serviceFeeController.text),
          'idTour': "3fa85f64-5717-4562-b3fc-2c963f66afa6",
          'idBusiness': '68554b5a-817b-453c-992c-149662a8e710'
        });
        print(body);
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

  void restore(TicketType data) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    final isValid = ticketTypeFormKey.currentState!.validate();
    if (!isValid) {
      return;
    }
    ticketTypeFormKey.currentState!.save();
    try {
      if (!isAdding) {
        TicketType ticket = TicketType(
            id: data.id,
            name: data.name,
            price: data.price,
            status: 3,
            commissionFeePercentage: data.commissionFeePercentage,
            serviceFeePercentage: data.serviceFeePercentage,
            idTour: data.idTour,
            idBusiness: '68554b5a-817b-453c-992c-149662a8e710');
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
      }
    } catch (error) {
      print(error);
    }
  }

  String? validate(String value, String message) {
    if (value.isEmpty) {
      return message;
    }
    return null;
  }
}
