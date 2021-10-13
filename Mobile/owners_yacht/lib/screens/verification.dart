import 'package:flutter/material.dart';
import 'package:get/get.dart';

import '/models/yacht.dart';
import '../widgets/app_bar.dart';
import '../widgets/verification_card.dart';

class Verification extends StatelessWidget {
  Verification({Key? key}) : super(key: key);
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: NavBar(
        title: 'Xác nhận',
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: ListView.builder(
          itemBuilder: (ctx, i) => VerificationCard(
              'dattestlayout', '1', 'https://www.w3schools.com/w3images/avatar2.png'),
          itemCount: 3,
        ),
      ),
    );
  }
}
