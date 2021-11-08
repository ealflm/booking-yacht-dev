import 'dart:math';

import 'package:get/get.dart';
import 'package:owners_yacht/models/tour.dart';
import 'package:owners_yacht/screens/tour_detail.dart';
import 'package:owners_yacht/screens/tours.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class TourController extends GetxController {
  var isLoading = true.obs;
  List<Tour> listTour = <Tour>[].obs;
  var tourDetail = Tour();

  @override
  onInit() {
    fectchsTour();
    super.onInit();
  }

  Future<List<Tour>> fectchsTour() async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/tours"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var tour = tourReponseFromJson(jsonString);
        if (tour.data != null) {
          listTour = tour.data as List<Tour>;
        }
      }
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return listTour;
  }

  void getTour() {
    if (listTour.isNotEmpty) {
      update();
      Get.to(Tours());
    } else {
      print('loi o get tour');
    }
  }

  Future<Tour> getTourDetail(String id) async {
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
        var jsonString = json.decode(response.body);
        tourDetail = Tour(
          id: jsonString['data']['id'],
          title: jsonString['data']['title'],
          status: jsonString['data']['status'] as int,
          descriptions: jsonString['data']['descriptions'],
          imageLink: jsonString['data']["imageLink"],
        );
        update();
        Get.to(TourDetail());
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return tourDetail;
  }
}
