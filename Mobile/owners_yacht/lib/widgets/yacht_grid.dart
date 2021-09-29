

import '../models/yacht.dart';
import 'package:flutter/material.dart';
import 'yacht_item.dart';

class YachtGrid extends StatelessWidget {
  final List<Yacht> _list = [
    Yacht(
      id: 'a',
      title: 'Tau du lich 1',
      description: 'Tau du lich 1Tau du lich 1',
      price: 29.99,
      imageUrl:
          'https://www.ferretti-yachts.com/uploadB2B/Models/Images/Main/Ferretti/medium/47591.jpg',
    ),
    Yacht(
      id: 'b',
      title: 'Tau du lich 2',
      description: 'Tau du lich 1Tau du lich 2',
      price: 59.99,
      imageUrl:
          'https://i1.wp.com/www.barcheamotore.com/wp-content/uploads/2019/10/Ferretti-Yachts-720_1.jpg?fit=900%2C500&ssl=1',
    ),
  ];
  @override
  Widget build(BuildContext context) {
    return GridView.builder(
      padding: const EdgeInsets.all(10.0),
      itemCount: _list.length,
      itemBuilder: (ctx, i) => YachtItem(
        _list[i].id,
        _list[i].title,
        _list[i].imageUrl,
      ),
      gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 2,
        childAspectRatio: 3 / 2,
        crossAxisSpacing: 10,
        mainAxisSpacing: 10,
      ),
    );
  }
}
