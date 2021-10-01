import '/widgets/login_title.dart';
import 'package:flutter/material.dart';

import '/widgets/login_button.dart';

class LoginScreen extends StatelessWidget {

  LoginScreen({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: NetworkImage(
                'https://www.srmg.com/storage/our-brands/daH7SHHJYxNlZbnT1PfGfk7JPNWDSyuAxJyiolWg.jpg?fbclid=IwAR16AliC00V57ByZajmSMgF8vvwtJnLOA-2Il5sltLUEfaUdLP_qN7QnZ9I'),
            fit: BoxFit.cover,
          ),
        ),
        child: SafeArea(
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 32.0),
            child: Column(
              children: [
                LoginTitle(title: 'Manager Yacht'),
                Spacer(),
                LoginGoogleButton(),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
