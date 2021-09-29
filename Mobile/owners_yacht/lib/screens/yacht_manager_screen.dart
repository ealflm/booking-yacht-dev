import 'package:flutter/material.dart';
import 'package:owners_yacht/models/yacht.dart';
import 'package:owners_yacht/screens/yacht_add_screen.dart';
import 'package:owners_yacht/widgets/app_drawer.dart';
import 'package:owners_yacht/widgets/yacht_card.dart';

class YachtManager extends StatelessWidget {
  List<Yacht> _list = [
    Yacht(
      id: 'a',
      title: 'Tau du lich 1',
      description: 'Tau du lich 1Tau du lich 1',
      price: 29.99,
      status: 'available',
      imageUrl:
          'https://www.ferretti-yachts.com/uploadB2B/Models/Images/Main/Ferretti/medium/47591.jpg',
    ),
    Yacht(
      id: 'b',
      title: 'Tau du lich 2',
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
        title: Text('Manager Yacht'),
        backgroundColor: Colors.black,
      ),
      drawer: AppDrawer(),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: ListView.builder(
          itemBuilder: (ctx, i) =>
              YachtCard(_list[i].title, _list[i].status, _list[i].imageUrl),
          itemCount: _list.length,
        ),
      ),
        floatingActionButton: FloatingActionButton(
        tooltip: 'Add',
        child: Icon(Icons.add, color: Colors.black),
        onPressed: (){
         Navigator.push(context,
                      MaterialPageRoute(builder: (context) => AddYacht()));
        },
        backgroundColor: Colors.white,
      ),
    );
  }
}
