import 'package:flutter/material.dart';
import 'package:owners_yacht/widgets/yacht_grid.dart';

class MainScreen extends StatefulWidget {
  const MainScreen({ Key? key }) : super(key: key);

  @override
  State<MainScreen> createState() => _MainScreenState();
}

class _MainScreenState extends State<MainScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('My Yacht'),
      ),
      body: YachtGrid(),
    );
    }
}