import 'package:flutter/material.dart';

class VerificationCard extends StatelessWidget {
  final String title;
  final String status;
  final String imgUrl;
  VerificationCard(this.title, this.status, this.imgUrl);
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
        trailing: SizedBox(
          width: 100,
          child: Row(
            children: <Widget>[
              IconButton(
                onPressed: () {},
                icon: const Icon(
                  Icons.check,
                  color: Colors.greenAccent,
                ),
              ),
              IconButton(
                onPressed: () {},
                icon: const Icon(
                  Icons.cancel,
                  color: Colors.redAccent,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
