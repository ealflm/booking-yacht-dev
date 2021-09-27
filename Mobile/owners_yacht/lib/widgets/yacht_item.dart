import 'package:flutter/material.dart';
import '../providers/yacht.dart';

class YachtItem extends StatelessWidget {
  final String id;
  final String title;
  final String imageUrl;

  YachtItem(this.id, this.title, this.imageUrl);

  @override
  Widget build(BuildContext context) {
    return ClipRRect(
      borderRadius: BorderRadius.circular(10),
      child: GridTile(
        child: GestureDetector(
          onTap: () {
            null;
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
              Icons.shopping_cart,
            ),
            onPressed: () {
              null;
            },
            color: Theme.of(context).accentColor,
          ),
        ),
      ),
    );
  }
}
