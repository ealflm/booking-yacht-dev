import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/trip.dart';

class Trips extends StatelessWidget {
  TripController controller = Get.find<TripController>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(controller.items[0].time.toString()),
      ),
      body: Container(),
    );
  }
}
