import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/transaction.dart';

import '/models/yacht.dart';
import '../widgets/nav_bar.dart';
import '../widgets/transaction_card.dart';

class Transactions extends StatelessWidget {
  Transactions({Key? key}) : super(key: key);
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const NavBar(
        title: 'Trang chủ',
        automaticallyImplyLeading: false,
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: GetBuilder<TransactionController>(
          builder: (controller) => (controller.isLoading.isTrue)
              ? const Center(child: CircularProgressIndicator())
              : Padding(
                  padding: const EdgeInsets.only(top: 10.0),
                  child: ListView.builder(
                    itemBuilder: (ctx, i) => TransactionCard(
                      transaction: controller.listTransaction[i],
                    ),
                    itemCount: controller.listTransaction.length,
                  ),
                ),
        ),
      ),
    );
  }
}
