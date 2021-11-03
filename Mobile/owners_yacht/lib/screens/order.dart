import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/order.dart';

import '/models/yacht.dart';
import '../widgets/nav_bar.dart';
import '../widgets/order_card.dart';

class Orders extends StatelessWidget {
  Orders({Key? key}) : super(key: key);
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const NavBar(
        title: 'Đơn đặt tàu',
        automaticallyImplyLeading: false,
      ),
      body: Padding(
        padding: const EdgeInsets.all(8.0),
        child: GetBuilder<OrderController>(
          builder: (controller) => (controller.isLoading.isTrue)
              ? const Center(child: CircularProgressIndicator())
              : controller.listOrders.isEmpty
                  ? const Center(child: Text('Không có đơn đặt tàu nào!'))
                  : Padding(
                      padding: const EdgeInsets.only(top: 10.0),
                      child: ListView.builder(
                        itemBuilder: (ctx, i) => OrderCard(
                          order: controller.listOrders[i],
                        ),
                        itemCount: controller.listOrders.length,
                      ),
                    ),
        ),
      ),
    );
  }
}
