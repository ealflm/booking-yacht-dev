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
  String id = "";
  bool isAdding = true;

  var categoryController = "";
  TextEditingController nameController = TextEditingController();
  TextEditingController seatController = TextEditingController();
  TextEditingController statusController = TextEditingController();
  TextEditingController descriptionsController = TextEditingController();

  // @override
  // onInit() {
  //   fetchYachts();
  //   getCategory();
  //   super.onInit();
  // }

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
        var jsonString = response.body;
        var yachts = yachtFromJson(jsonString);
        if (yachts.data != null) {
          listYacht = yachts.data as List<Yacht>;
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
          listCategory = categorys.data as List<Category>;
          print(listCategory[0].id);
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
    isAdding = false;
    id = yacht.id!;
    categoryController = yacht.idVehicleType!;
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
    isAdding = true;
    nameController.clear();
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
            seat: int.parse(seatController.text),
            descriptions: descriptionsController.text,
            idVehicleType: categoryController,
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
          getYachtDetail(id);
          fetchYachts();
          update();
          Get.back();
        } else {
          print('loi o save roi ');
        }
      } else {
        Yacht yacht = Yacht(
            name: nameController.text,
            seat: int.parse(seatController.text),
            descriptions: descriptionsController.text,
            idVehicleType: categoryController,
            idBusiness: '26f7f596-a747-4965-8cb1-36eadd73ee49',
            status: int.parse(statusController.text));
        String body = json.encode(yacht);

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
