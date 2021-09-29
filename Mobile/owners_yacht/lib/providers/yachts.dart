

import 'package:flutter/material.dart';

import '../models/yacht.dart';

class Yachts{
  List<Yacht> _list = [
    Yacht(
      id: 'a',
      title: 'Tau du lich 1',
      description: 'Tau du lich 1Tau du lich 1',
      price: 99.99,
      imageUrl:
          'https://www.ferretti-yachts.com/uploadB2B/Models/Images/Main/Ferretti/medium/47591.jpg',
    ),
    Yacht(
      id: 'b',
      title: 'Tau du lich 2',
      description: 'Tau du lich 1Tau du lich 2',
      price: 22.22,
      imageUrl:
          'https://i1.wp.com/www.barcheamotore.com/wp-content/uploads/2019/10/Ferretti-Yachts-720_1.jpg?fit=900%2C500&ssl=1',
    ),
  ];

   List<Yacht> get items {
    return [..._list];
  }
}