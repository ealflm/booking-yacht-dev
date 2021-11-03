import 'package:flutter/material.dart';
import '/models/yacht.dart';
import '../widgets/nav_bar.dart';
import '../widgets/tour_card.dart';

class History extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return const Scaffold(
      appBar: NavBar(
        title: 'History',
        automaticallyImplyLeading: false,
      ),
      //     body: Padding(
      //       padding: const EdgeInsets.all(8.0),
      //       child: ListView.builder(
      //         itemBuilder: (ctx, i) => TourCard(_list[i].title, _list[i].status,
      //             _list[i].imageUrl, _list[i].price),
      //         itemCount: _list.length,
      //       ),
      //     ),
    );
  }
}
