import 'package:flutter/material.dart';
import '../widgets/app_drawer.dart';
import '/widgets/yacht_grid.dart';

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
          'Yacht Manager',
        ),
        
        backgroundColor: Colors.black,
      ),
      drawer: AppDrawer(),
      body: YachtGrid(),
    );
  }
}
