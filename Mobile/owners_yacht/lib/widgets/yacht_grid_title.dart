import 'package:flutter/material.dart';
import 'package:owners_yacht/screens/yacht_detail_screen.dart';

class YachtTitle extends StatelessWidget {
  final String id;
  final String title;
  final String imageUrl;

  YachtTitle(this.id, this.title, this.imageUrl);

  @override
  Widget build(BuildContext context) {
    return ClipRRect(
      borderRadius: BorderRadius.circular(10),
      child: GridTile(
        child: GestureDetector(
          onTap: () {
            Navigator.push(context,
                MaterialPageRoute(builder: (context) => YachtDetail()));
          },
          child: Image.network(
            imageUrl,
            fit: BoxFit.cover,
          ),
        ),
        footer: GridTileBar(
          backgroundColor: Colors.black87,
          title: Text(
            title,
            textAlign: TextAlign.center,
          ),
          trailing: IconButton(
            icon: Icon(
              Icons.circle,
              size: 20,
            ),
            onPressed: () {},
            color: Colors.orange[800],
          ),
        ),
      ),
    );
  }
}
