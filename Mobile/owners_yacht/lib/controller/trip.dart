import 'package:get/get.dart';
import '../screens/trips.dart';
import '/models/trip.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class TripController extends GetxController {
  var isLoading = true.obs;
  List<Trip> items = <Trip>[].obs;

  Future<List<Trip>?> loadTrip() async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/trips"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var trips = tripReponseFromJson(jsonString);
        if (trips.data != null) {
          items = trips.data as List<Trip>;
        }
        print(items[0].time.toString());
        update();
        Get.to(Trips());
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
}
