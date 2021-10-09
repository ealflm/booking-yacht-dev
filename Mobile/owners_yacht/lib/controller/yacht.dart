import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/screens/yacht_detail.dart';
import 'package:owners_yacht/screens/yacht_modify.dart';
import 'package:owners_yacht/screens/yachts.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '/models/yacht.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class YachtController extends GetxController {
  var isLoading = true.obs;
  List<Yacht> items = <Yacht>[].obs;
  List<Yacht> yachtDetail = <Yacht>[].obs;

  TextEditingController nameController = TextEditingController();
  TextEditingController seatController = TextEditingController();
  TextEditingController statusController = TextEditingController();
  TextEditingController descriptionsController = TextEditingController();

  @override
  onInit() {
    fetchYachts();
    super.onInit();
  }

  Future<List<Yacht>?> fetchYachts() async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var yachts = yachtFromJson(jsonString);
        if (yachts.data != null) {
          items = yachts.data as List<Yacht>;
        }
        update();
      } else {
        return null;
      }
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return items;
  }

  Future<List<Yacht>?> getYacht(String id) async {
    yachtDetail = <Yacht>[];
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/${id}"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = json.decode(response.body);
        print(jsonString['data']['descriptions'].toString());
        yachtDetail.add(Yacht(
            id: jsonString['data']['id'],
            name: jsonString['data']['name'],
            seat: jsonString['data']['seat'] as int,
            descriptions: jsonString['data']['descriptions'],
            idVehicleType: jsonString['data']['idVehicleType'],
            idBusiness: jsonString['data']['idBusiness'],
            status: jsonString['data']['status'] as int));
        update();
        Get.to(YachtDetail());
      } else {
        return null;
      }
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return yachtDetail;
  }

  void editYacht(Yacht yacht) async {
    nameController.text = yacht.name;
    seatController.text = yacht.seat.toString();
    statusController.text = yacht.status.toString();
    descriptionsController.text = yacht.descriptions;
    Get.to(YachtModify());
  }

  void deleteYacht(String id) async {
    print(id);
  }
  void save() async {
    print(nameController.text);
    print(seatController.text);
  }
}
