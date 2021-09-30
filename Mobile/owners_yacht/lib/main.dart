import 'package:flutter/material.dart';
// import 'package:provider/provider.dart';
// import 'providers/login_google.dart';
import 'screens/login.dart';

void main() => runApp(OwnersYacht());

class OwnersYacht extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      initialRoute: '/',
      home: LoginScreen(),
      // routes: {

      // },
    );
  }
}
