import 'package:flutter/material.dart';
// import 'package:provider/provider.dart';
// import 'providers/login_google.dart';
import 'screens/login_screen.dart';

void main() => runApp(OwnersYacht());

class OwnersYacht extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Manager Yacht',
      theme: ThemeData(
          //  primarySwatch: Colors.,
          ),
      initialRoute: '/',
      home: LoginScreen(),
      // routes: {

      // },
    );
  }
}
