import 'package:flutter/material.dart';
// import 'package:provider/provider.dart';
// import 'providers/login_google.dart';
import 'screens/login_screen.dart';

void main() => runApp(const OwnersYacht());

class OwnersYacht extends StatelessWidget {
  const OwnersYacht({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      title: 'Manager Yacht',
      theme: ThemeData(
        //  primarySwatch: Colors.,
      ),
      home: LoginScreen(),
    );
  }
}
