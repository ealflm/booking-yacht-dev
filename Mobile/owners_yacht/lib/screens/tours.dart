import 'package:flutter/material.dart';
import 'package:owners_yacht/controller/tour.dart';
import 'package:owners_yacht/widgets/nav_bar.dart';
import 'package:owners_yacht/widgets/tour_card.dart';
import 'package:get/get.dart';

class Tours extends StatelessWidget {
  TourController _tourController = Get.find<TourController>();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const NavBar(
        title: 'Tour',
        automaticallyImplyLeading: true,
      ),
      body: (_tourController.isLoading == true)
          ? const Center(child: CircularProgressIndicator())
          : _tourController.listTour.isEmpty
              ? const Center(child: Text('Không có tour nào!'))
              : Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: ListView.builder(
                    itemBuilder: (ctx, i) => TourCard(
                      tour: _tourController.listTour[i],
                    ),
                    itemCount: _tourController.listTour.length,
                  ),
                ),
    );
  }
}
