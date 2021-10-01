import 'package:flutter/material.dart';
import 'package:owners_yacht/screens/home_screen.dart';
import 'package:owners_yacht/screens/yacht_add_screen.dart';
import 'package:owners_yacht/screens/yacht_manager_screen.dart';

class AppDrawer extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Container(
      width: 230,
      child: Drawer(
        child: ListView(
          children: <Widget>[
            AppBar(
              title: Text(
                'Name: ',
                style: TextStyle(color: Colors.black),
              ),
              automaticallyImplyLeading: false,
              backgroundColor: Colors.white,
            ),
            Divider(),
            ListTile(
                leading: Icon(Icons.home),
                title: Text('Home'),
                onTap: () {
                  Navigator.push(context,
                      MaterialPageRoute(builder: (context) => HomeScreen()));
                }),
            Divider(),
            ListTile(
                leading: Icon(Icons.home),
                title: Text('Yacht Manager'),
                onTap: () {
                  Navigator.push(context,
                      MaterialPageRoute(builder: (context) => YachtManager()));
                }),
            Divider(),
            ListTile(
              leading: Icon(Icons.tour),
              title: Text('Add tour'),
              onTap: () {},
            ),
            Divider(),
            ListTile(
              leading: Icon(Icons.qr_code_2),
              title: Text('Scan QR'),
              onTap: () {},
            ),
            Divider(),
            ListTile(
              leading: Icon(Icons.view_list_rounded),
              title: Text('Manage trip'),
              onTap: () {},
            ),
            Divider(),
            ListTile(
              leading: Icon(Icons.person),
              title: Text('Profile'),
              onTap: () {},
            ),
            Divider(),
            ListTile(
              leading: Icon(Icons.exit_to_app),
              title: Text('Logout'),
              onTap: () {},
            ),
            Divider(),
          ],
        ),
      ),
    );
  }
}
