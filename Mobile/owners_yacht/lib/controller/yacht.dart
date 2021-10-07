import 'package:get/get.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '/models/yacht.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';

class YachtController extends GetxController {
  var isLoading = true.obs;
  List<Yacht> items = <Yacht>[].obs;

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
        Uri.parse("https://booking-yacht.azurewebsites.net/api/v3/vehicle"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        print(jsonString);
        var yachts = yachtFromJson(jsonString);
        if (yachts != null) {
          items = yachts as List<Yacht>;
        }
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
