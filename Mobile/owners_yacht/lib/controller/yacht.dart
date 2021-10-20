import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/models/category.dart';
import 'package:owners_yacht/screens/yacht_detail.dart';
import 'package:owners_yacht/screens/yacht_modify.dart';
import 'package:owners_yacht/screens/yachts.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '/models/yacht.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class YachtController extends GetxController {
  var isLoading = true.obs;

  List<Yacht> listYacht = <Yacht>[].obs;
  List<Category> listCategory = <Category>[].obs;
  var yachtDetail = Yacht();

  bool isAdding = true;

  String id = "";
  String idBusiness = "";
  var categoryController = "";
  TextEditingController nameController = TextEditingController();
  TextEditingController seatController = TextEditingController();
  TextEditingController registrationNumberController = TextEditingController();
  TextEditingController yearOfManufactureController = TextEditingController();
  TextEditingController whereProductionController = TextEditingController();
  TextEditingController statusController = TextEditingController();
  TextEditingController descriptionsController = TextEditingController();

  void changeCategory(value) {
    categoryController = value;
    print(value);
    update();
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
        var yachts = yachtReponseFromJson(response.body);
        if (yachts.data != null) {
          listYacht = yachts.data as List<Yacht>;
          idBusiness = listYacht[0].idBusiness!;
        }
        getCategory();
        update();
        Get.to(Yachts());
      } else {
        return null;
      }
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return listYacht;
  }

  Future<List<Category>?> getCategory() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicle-types"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var categorys = categoryReponseFromJson(jsonString);
        if (categorys.data.isNotEmpty) {
          listCategory = categorys.data;
        }
      } else {
        return null;
      }
    } catch (error) {
      print('loi r');
    }
    return listCategory;
  }

  Future<Yacht> getYachtDetail(String id) async {
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
        // var yachts = yachtReponseFromJson(response.body);
        // if (yachts.data != null) {
        //   yachtDetail = yachts.data as Yacht;
        // }
        var jsonString = json.decode(response.body);

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
    isAdding = false;
    id = yacht.id!;
    categoryController = yacht.idVehicleType!;
    nameController.text = yacht.name!;
    registrationNumberController.text = yacht.registrationNumber.toString();
    yearOfManufactureController.text = yacht.yearOfManufacture.toString();
    seatController.text = yacht.seat.toString();
    statusController.text = yacht.status.toString();
    descriptionsController.text = yacht.descriptions!;
    Get.to(YachtModify());
  }

  void addYacht() async {
    isAdding = true;
    nameController.clear();
    registrationNumberController.clear();
    yearOfManufactureController.clear();
    whereProductionController.clear();
    seatController.clear();
    statusController.clear();
    categoryController = "";
    descriptionsController.clear();
    Get.to(YachtModify());
  }

  void deleteYacht(String id) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
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
      if (!isAdding) {
        Yacht yacht = Yacht(
            id: id,
            name: nameController.text,
            registrationNumber: registrationNumberController.text,
            yearOfManufacture: int.parse(yearOfManufactureController.text),
            whereProduction: whereProductionController.text,
            seat: int.parse(seatController.text),
            descriptions: descriptionsController.text,
            idVehicleType: categoryController,
            idBusiness: '68554b5a-817b-453c-992c-149662a8e710',
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
          getYachtDetail(id);
          fetchYachts();
          update();
          Get.back();
        } else {
          print('loi o save roi ');
        }
      } else {
        String body = json.encode({
          'name': nameController.text,
          'seat': int.parse(seatController.text),
          'registrationNumber': registrationNumberController.text,
          'yearOfManufacture': int.parse(yearOfManufactureController.text),
          'whereProduction': whereProductionController.text,
          'descriptions': descriptionsController.text,
          'idVehicleType': categoryController,
          'idBusiness': '68554b5a-817b-453c-992c-149662a8e710',
          'status': int.parse(statusController.text)
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
