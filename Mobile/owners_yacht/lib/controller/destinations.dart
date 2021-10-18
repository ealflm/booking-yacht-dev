import 'package:get/get.dart';
import 'package:owners_yacht/models/destinations.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class TicketController extends GetxController {
  var isLoading = true.obs;
  List<Destinations> listDestinations = <Destinations>[].obs;
  var destinationsDetail = Destinations();

  Future<List<Destinations>> getTicket() async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/destinations"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var destinations = destinationsReponseFromJson(jsonString);
        if (destinations.data != null) {
          listDestinations = destinations.data as List<Destinations>;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return listDestinations;
  }

  Future<Destinations> getTicketDetail(String id) async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/destinations/${id}"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var destinations = destinationsReponseFromJson(jsonString);
        if (destinations.data != null) {
          destinationsDetail = destinations.data as Destinations;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return destinationsDetail;
  }
}
