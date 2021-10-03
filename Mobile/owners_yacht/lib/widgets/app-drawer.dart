import 'package:flutter/material.dart';
import 'package:owners_yacht/screens/verification.dart';
import 'package:owners_yacht/screens/history.dart';
import 'package:owners_yacht/screens/home.dart';
import 'package:owners_yacht/screens/tour-detail.dart';
import 'package:owners_yacht/screens/tour.dart';
import 'package:owners_yacht/screens/yacht-modify.dart';
import 'package:owners_yacht/screens/yacht-manager.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';

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
                  Navigator.push(
                      context, MaterialPageRoute(builder: (context) => Home()));
                }),
            Divider(),
            ListTile(
                leading: FaIcon(
                  FontAwesomeIcons.ship,
                  size: 20,
                ),
                title: Text('Yacht'),
                onTap: () {
                  Navigator.push(context,
                      MaterialPageRoute(builder: (context) => YachtManager()));
                }),
            Divider(),
            ListTile(
              leading: Icon(Icons.tour),
              title: Text('Tour'),
              onTap: () {
                Navigator.push(
                    context, MaterialPageRoute(builder: (context) => Tour()));
              },
            ),
            Divider(),
            ListTile(
              leading: Icon(Icons.qr_code_2),
              title: Text('Scan QR'),
              onTap: () {},
            ),
            Divider(),
            ListTile(
              leading: Icon(Icons.checklist_rtl),
              title: Text('Verification'),
              onTap: () {
                Navigator.push(context,
                    MaterialPageRoute(builder: (context) => Verification()));
              },
            ),
            Divider(),
            ListTile(
              leading: Icon(Icons.history),
              title: Text('History'),
              onTap: () {
                Navigator.push(context,
                    MaterialPageRoute(builder: (context) => History()));
              },
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
