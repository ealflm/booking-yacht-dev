import 'package:get/get.dart';
import 'package:owners_yacht/models/destination_tour.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class TicketController extends GetxController {
  var isLoading = true.obs;
  List<DestinationTour> listDestinationTour = <DestinationTour>[].obs;
  var destinationTourDetail = DestinationTour();

  Future<List<DestinationTour>> getTicket() async {
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
        var destinationTour = destinationTourReponseFromJson(jsonString);
        if (destinationTour.data != null) {
          listDestinationTour = destinationTour.data as List<DestinationTour>;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return listDestinationTour;
  }

  Future<DestinationTour> getTicketDetail(String id) async {
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
        var destinationTour = destinationTourReponseFromJson(jsonString);
        if (destinationTour.data != null) {
          destinationTourDetail = destinationTour.data as DestinationTour;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return destinationTourDetail;
  }
}
