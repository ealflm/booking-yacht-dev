import 'package:flutter/material.dart';
import 'package:get/get.dart';
import '../screens/yacht_detail.dart';
import '../screens/tour_detail.dart';
import '../screens/yacht_modify.dart';

class TourCard extends StatelessWidget {
  final String title;
  final String status;
  final String imgUrl;
  final double price;

  TourCard(this.title, this.status, this.imgUrl, this.price);
  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        Get.to(TourDetail());
      },
      child: Card(
        elevation: 2,
        child: ListTile(
          leading: CircleAvatar(
            backgroundImage: NetworkImage(imgUrl),
          ),
          title: Text(title),
          subtitle: Text(status),
          trailing: Text('Price: \$${price}'),
        ),
      ),
    );
  }
}
