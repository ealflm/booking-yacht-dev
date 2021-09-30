import 'package:flutter/material.dart';
import 'package:owners_yacht/models/yacht.dart';
import 'package:owners_yacht/widgets/app-drawer.dart';
import 'package:owners_yacht/widgets/tour-card.dart';

class Tour extends StatelessWidget {
  // const ManagerTour({Key? key}) : super(key: key);
  List<Yacht> _list = [
    Yacht(
      id: 'a',
      title: 'Tour 1',
      description: 'Tau du lich 1Tau du lich 1',
      price: 29.99,
      status: 'available',
      imageUrl:
          'https://www.ferretti-yachts.com/uploadB2B/Models/Images/Main/Ferretti/medium/47591.jpg',
    ),
    Yacht(
      id: 'b',
      title: 'Tour 2',
      description: 'Tau du lich 1Tau du lich 2',
      price: 59.99,
      status: 'available',
      imageUrl:
          'https://i1.wp.com/www.barcheamotore.com/wp-content/uploads/2019/10/Ferretti-Yachts-720_1.jpg?fit=900%2C500&ssl=1',
    ),
  ];

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Tour'),
        backgroundColor: Colors.black,
      ),
      drawer: AppDrawer(),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: ListView.builder(
          itemBuilder: (ctx, i) =>
              TourCard(_list[i].title, _list[i].status, _list[i].imageUrl),
          itemCount: _list.length,
        ),
      ),
    );
  }
}
