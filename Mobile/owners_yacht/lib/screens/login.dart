import 'package:flutter/material.dart';

import '../widgets/login-button.dart';

class Login extends StatelessWidget {
  Login({Key? key}) : super(key: key);

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
                Padding(
                  padding: const EdgeInsets.only(top: 80),
                  child: Text(
                    'Manager Yacht',
                    style: TextStyle(
                        fontSize: 40,
                        fontWeight: FontWeight.bold,
                        color: Colors.white),
                  ),
                ),
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
