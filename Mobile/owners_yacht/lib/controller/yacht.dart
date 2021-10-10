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
  var yachtDetail = Yacht();
  String id = "";
  String type = "";
  bool isAdd = true;
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

  Future<Yacht> getYacht(String id) async {
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
        yachtDetail = Yacht(
            id: jsonString['data']['id'],
            name: jsonString['data']['name'],
            seat: jsonString['data']['seat'] as int,
            descriptions: jsonString['data']['descriptions'],
            idVehicleType: jsonString['data']['idVehicleType'],
            idBusiness: jsonString['data']['idBusiness'],
            status: jsonString['data']['status'] as int);
        update();
        Get.to(YachtDetail());
      } else {
        // return null;
      }
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return yachtDetail;
  }

  void editYacht(Yacht yacht) async {
    isAdd = false;
    id = yacht.id!;
    type = yacht.idVehicleType!;
    nameController.text = yacht.name!;
    seatController.text = yacht.seat.toString();
    statusController.text = yacht.status.toString();
    descriptionsController.text = yacht.descriptions!;
    print('id business');
    print(yacht.idBusiness);

    print('object');
    print(yacht.idVehicleType);
    Get.to(YachtModify());
  }

  void addYacht() async {
    isAdd = true;
    nameController.clear();
    seatController.clear();
    statusController.clear();
    descriptionsController.clear();
    Get.to(YachtModify());
  }

  void deleteYacht(String id) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final SharedPreferences prefs = await SharedPreferences.getInstance();
      final String? token = prefs.getString('token');
      final response = await http.delete(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/${id}"),
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer $token",
          });
      if (response.statusCode == 200) {
        fetchYachts();
        update();
        Get.back();
      } else {
        print('loi o delete');
      }
    } catch (error) {
      print(error);
    }
  }

  void save() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');

    try {
      if (!isAdd) {
        Yacht yacht = Yacht(
            id: id,
            name: nameController.text,
            seat: int.parse(seatController.text),
            descriptions: descriptionsController.text,
            idVehicleType: '3fa85f64-5717-4562-b3fc-2c963f66afa6',
            idBusiness: '26f7f596-a747-4965-8cb1-36eadd73ee49',
            status: int.parse(statusController.text));
        String body = json.encode(yacht);

        final response = await http.put(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/${id}"),
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer $token",
          },
          body: body,
        );
        if (response.statusCode == 200) {
          getYacht(id);
          fetchYachts();
          update();
          Get.back();
        } else {
          print('loi o save roi ');
        }
      } else {
        String body = json.encode({
          'name': nameController.text,
          'seat': seatController.text,
          'descriptions': descriptionsController.text,
          'idVehicleType': '3fa85f64-5717-4562-b3fc-2c963f66afa6',
          'idBusiness': '26f7f596-a747-4965-8cb1-36eadd73ee49',
          'status': statusController.text,
        });

        final response = await http.post(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles"),
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer $token",
          },
          body: body,
        );
        if (response.statusCode == 200) {
          fetchYachts();
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
}
