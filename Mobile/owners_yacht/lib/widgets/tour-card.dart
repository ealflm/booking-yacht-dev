import 'package:flutter/material.dart';

import 'package:flutter/material.dart';
import 'package:owners_yacht/screens/yacht-detail.dart';
import 'package:owners_yacht/screens/tour-detail.dart';
import 'package:owners_yacht/screens/yacht-modify.dart';

class TourCard extends StatelessWidget {
  final String title;
  final String status;
  final String imgUrl;

  TourCard(this.title, this.status, this.imgUrl);
  @override
  Widget build(BuildContext context) {
    return GestureDetector(
      onTap: () {
        Navigator.push(
            context, MaterialPageRoute(builder: (context) => TourDetail()));
      },
      child: Card(
        elevation: 2,
        child: ListTile(
          leading: CircleAvatar(
            backgroundImage: NetworkImage(imgUrl),
          ),
          title: Text(title),
          subtitle: Text(status),
          trailing: Text('Price: \$33'),
        ),
      ),
    );
  }
}
