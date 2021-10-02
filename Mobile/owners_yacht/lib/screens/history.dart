import 'package:flutter/material.dart';
import 'package:owners_yacht/models/yacht.dart';
import 'package:owners_yacht/widgets/app-bar.dart';
import 'package:owners_yacht/widgets/app-drawer.dart';
import 'package:owners_yacht/widgets/tour-card.dart';

class History extends StatelessWidget {
  History({Key? key}) : super(key: key);
  List<Yacht> _list = [
    Yacht(
      id: 'a',
      title: 'Tour 1 done',
      description: 'Tau du lich 1Tau du lich 1',
      price: 29.99,
      status: 'Đã đi ngày: 12/08/2021',
      imageUrl:
          'https://www.ferretti-yachts.com/uploadB2B/Models/Images/Main/Ferretti/medium/47591.jpg',
    ),
    Yacht(
      id: 'b',
      title: 'Tour 2 done',
      description: 'Tau du lich 1Tau du lich 2',
      price: 59.99,
      status: 'Đã đi ngày: 11/08/2021',
      imageUrl:
          'https://i1.wp.com/www.barcheamotore.com/wp-content/uploads/2019/10/Ferretti-Yachts-720_1.jpg?fit=900%2C500&ssl=1',
    ),
    Yacht(
      id: 'c',
      title: 'Tour 3 done',
      description: 'Tau du lich 1Tau du lich 2',
      price: 59.99,
      status: 'Đã đi ngày: 12/02/2021',
      imageUrl:
          'https://i1.wp.com/www.barcheamotore.com/wp-content/uploads/2019/10/Ferretti-Yachts-720_1.jpg?fit=900%2C500&ssl=1',
    ),
    Yacht(
      id: 'd',
      title: 'Tour 4 done',
      description: 'Tau du lich 1Tau du lich 2',
      price: 59.99,
      status: 'Đã đi ngày: 1/01/2021',
      imageUrl:
          'https://i1.wp.com/www.barcheamotore.com/wp-content/uploads/2019/10/Ferretti-Yachts-720_1.jpg?fit=900%2C500&ssl=1',
    ),
  ];
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: NavBar(
        title: 'History',
      ),
      drawer: AppDrawer(),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: ListView.builder(
          itemBuilder: (ctx, i) => TourCard(_list[i].title, _list[i].status,
              _list[i].imageUrl, _list[i].price),
          itemCount: _list.length,
        ),
      ),
    );
  }
}
