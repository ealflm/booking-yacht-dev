import 'package:get/get.dart';
import 'package:owners_yacht/models/ticket.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:http/http.dart' as http;

class TicketController extends GetxController {
  var isLoading = true.obs;
  List<Ticket> listTicket = <Ticket>[].obs;
  var ticketDetail = Ticket();

  Future<List<Ticket>> getTicket() async {
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
        var ticket = ticketReponseFromJson(jsonString);
        if (ticket.data != null) {
          listTicket = ticket.data as List<Ticket>;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return listTicket;
  }

  Future<Ticket> getTicketDetail(String id) async {
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
        var ticket = ticketReponseFromJson(jsonString);
        if (ticket.data != null) {
          ticketDetail = ticket.data as Ticket;
        }
        update();
        // Get.to();
      } else {}
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return ticketDetail;
  }
}
