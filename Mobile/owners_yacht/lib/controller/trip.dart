import 'dart:convert';

import 'package:flutter_local_notifications/flutter_local_notifications.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/models/business_tour.dart';
import 'package:owners_yacht/screens/trip_modify.dart';
import '../screens/trips.dart';
import '/models/trip.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class TripController extends GetxController {
  var isLoading = true.obs;
  // bool isAdding = true;
  String id = "";
  List<BusinessTour> listBusinessTour = <BusinessTour>[].obs;
  @override
  onInit() {
    getBusinessTour(DateTime.now());
    super.onInit();
  }

  Future<List<BusinessTour>?> getBusinessTour(DateTime time) async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    final String? idBusiness = prefs.getString('idBusiness');
    try {
      Map<String, String> queryParams = {
        'idBusiness': idBusiness!,
        'time': time.toString(),
      };

      final response = await http.get(
        Uri.parse(
                "https://booking-yacht.azurewebsites.net/api/v1.0/business/business-tours")
            .replace(queryParameters: queryParams),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      print(response.statusCode);
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var businessTour = businessTourReponseFromJson(jsonString);
        if (businessTour.data != null) {
          listBusinessTour = businessTour.data as List<BusinessTour>;
        }
        print('load done');
        update();
      } else {
        return null;
      }
    } catch (error) {
    } finally {
      isLoading(false);
    }
    return listBusinessTour;
  }

  // void editYacht(Trip trip) async {
  //   isAdding = false;
  //   id = trip.id!;
  //   Get.to(TripModify());
  // }

  // void addYacht() async {
  //   isAdding = true;

  //   Get.to(TripModify());
  // }

  // void save() async {
  //   final SharedPreferences prefs = await SharedPreferences.getInstance();
  //   final String? token = prefs.getString('token');

  //   try {
  //     if (!isAdding) {
  //       Trip trip = Trip(
  //           id: '1',
  //           time: DateTime.utc(2010, 11, 9),
  //           idBusiness: '26f7f596-a747-4965-8cb1-36eadd73ee49',
  //           idVehicle: '4daf872a-5970-4274-9c10-33b5b726fd2c',
  //           status: 1);
  //       String body = json.encode(trip);

  //       final response = await http.put(
  //         Uri.parse(
  //             "https://booking-yacht.azurewebsites.net/api/v1.0/business/trips/${id}"),
  //         headers: {
  //           "Content-Type": "application/json",
  //           "Authorization": "Bearer $token",
  //         },
  //         body: body,
  //       );
  //       if (response.statusCode == 200) {
  //         getTrip();
  //         update();
  //         Get.back();
  //       } else {
  //         print('loi o save roi ');
  //       }
  //     } else {
  //       Trip trip = Trip(
  //           time: DateTime.utc(2010, 11, 9),
  //           idBusiness: '26f7f596-a747-4965-8cb1-36eadd73ee49',
  //           idVehicle: '4daf872a-5970-4274-9c10-33b5b726fd2c',
  //           status: 1);
  //       String body = json.encode(trip);
  //       final response = await http.post(
  //         Uri.parse(
  //             "https://booking-yacht.azurewebsites.net/api/v1.0/business/trips"),
  //         headers: {
  //           "Content-Type": "application/json",
  //           "Authorization": "Bearer $token",
  //         },
  //         body: body,
  //       );
  //       if (response.statusCode == 200) {
  //         getTrip();
  //         update();
  //         Get.back();
  //       } else {
  //         print('loi o save roi ');
  //       }
  //     }
  //   } catch (error) {
  //     print(error);
  //   }
  // }

  // void deleteYacht(String id) async {
  //   final SharedPreferences prefs = await SharedPreferences.getInstance();
  //   final String? token = prefs.getString('token');
  //   try {
  //     final response = await http.delete(
  //         Uri.parse(
  //             "https://booking-yacht.azurewebsites.net/api/v1.0/business/trips/${id}"),
  //         headers: {
  //           "Content-Type": "application/json",
  //           "Authorization": "Bearer $token",
  //         });
  //     if (response.statusCode == 200) {
  //       getTrip();
  //       update();
  //       Get.back();
  //     } else {
  //       print('loi o delete');
  //     }
  //   } catch (error) {
  //     print(error);
  //   }
  // }
}
