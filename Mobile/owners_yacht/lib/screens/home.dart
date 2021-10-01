import 'package:flutter/material.dart';
import '../widgets/app-drawer.dart';
import '../widgets/yacht-grid.dart';

class HomeScreen extends StatefulWidget {
  const HomeScreen({Key? key}) : super(key: key);
  static const routeName = '/home';
  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(
          'Manager',
        ),
        
        backgroundColor: Colors.black,
      ),
      drawer: AppDrawer(),
      body: YachtGrid(),
    );
  }
}
