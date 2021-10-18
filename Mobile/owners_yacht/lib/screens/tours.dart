import 'package:flutter/material.dart';
import 'package:owners_yacht/widgets/app_bar.dart';
import 'package:owners_yacht/widgets/tour_card.dart';

class Tours extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const NavBar(
        title: 'Tour',
        automaticallyImplyLeading: true,
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: ListView.builder(
          itemBuilder: (ctx, i) => TourCard(),
          itemCount: 3,
        ),
      ),
    );
  }
}
