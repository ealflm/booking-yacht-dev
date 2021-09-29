import 'package:flutter/material.dart';

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
        leading: CircleAvatar(
            backgroundImage: NetworkImage(imgUrl),
            ),
        title: Text(title),
        subtitle: Text(status),
        trailing: Container(
          width: 100,
          child: Row(
            children: <Widget>[
              IconButton(
                onPressed: () {},
                icon: Icon(Icons.edit),
              ),
              IconButton(
                onPressed: () {},
                icon: Icon(Icons.delete),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
