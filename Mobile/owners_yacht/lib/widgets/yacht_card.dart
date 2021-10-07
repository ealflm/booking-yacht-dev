import 'package:flutter/material.dart';
import 'package:owners_yacht/models/yacht.dart';
import 'package:owners_yacht/screens/yacht_detail.dart';
import 'package:owners_yacht/screens/yacht_modify.dart';
import 'package:get/get.dart';

class YachtCard extends StatelessWidget {
  final Yacht yacht;

  const YachtCard(this.yacht);

  @override
  Widget build(BuildContext context) {
    print('screen');
    print(yacht.name);
    return Card(
      elevation: 2,
      child: ListTile(
        leading: GestureDetector(
          child: CircleAvatar(
            backgroundImage: NetworkImage(
                'https://www.ferretti-yachts.com/uploadB2B/Models/Images/Main/Ferretti/medium/47591.jpg'),
          ),
          onTap: () {
            Get.toNamed('/yacht-detail', arguments: yacht.name);
          },
        ),
        title: Text(yacht.name),
        subtitle: Text((yacht.status != null) ? '${yacht.status}' : 'No'),
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
