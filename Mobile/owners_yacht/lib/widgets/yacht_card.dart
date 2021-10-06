import 'package:flutter/material.dart';
import 'package:owners_yacht/screens/yacht_detail.dart';
import 'package:owners_yacht/screens/yacht_modify.dart';
import 'package:get/get.dart';

class YachtCard extends StatelessWidget {
  final String title;
  final String status;
  final String imgUrl;
  YachtCard(this.title, this.status, this.imgUrl);
  @override
  Widget build(BuildContext context) {
    return Card(
      elevation: 2,
      child: ListTile(
        leading: GestureDetector(
          child: CircleAvatar(
            backgroundImage: NetworkImage(imgUrl),
          ),
          onTap: () {
            Get.toNamed('/yacht-detail', arguments: title);
          },
        ),
        title: Text(title),
        subtitle: Text(status),
        trailing: Container(
          width: 100,
          child: Row(
            children: <Widget>[
              IconButton(
                onPressed: () {
                  Get.to(ModifyYacht('Edit Yacht'));
                },
                icon: Icon(Icons.edit, color: Colors.blue),
              ),
              IconButton(
                onPressed: () {},
                icon: Icon(Icons.delete, color: Colors.redAccent),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
