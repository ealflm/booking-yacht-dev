import 'package:get/get.dart';
import 'package:owners_yacht/models/place_type.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class TicketController extends GetxController {
  var isLoading = true.obs;
  List<PlaceType> listPlaceType = <PlaceType>[].obs;
  var placeTypeDetail = PlaceType();

  Future<List<PlaceType>> getPlaceType() async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/ticket"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var placeType = placeTypeReponseFromJson(jsonString);
        if (placeType.data != null) {
          listPlaceType = placeType.data as List<PlaceType>;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return listPlaceType;
  }

  Future<PlaceType> getPlaceTypeDetail(String id) async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/ticket/${id}"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var placeType = placeTypeReponseFromJson(jsonString);
        if (placeType.data != null) {
          placeTypeDetail = placeType.data as PlaceType;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return placeTypeDetail;
  }
}
