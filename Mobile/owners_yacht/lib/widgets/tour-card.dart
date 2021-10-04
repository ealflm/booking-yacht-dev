import 'package:flutter/material.dart';
import 'package:get/get.dart';
import '/screens/yacht-detail.dart';
import '/screens/tour-detail.dart';
import '/screens/yacht-modify.dart';

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
