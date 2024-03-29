import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/ticket_type.dart';
import 'package:owners_yacht/widgets/nav_bar.dart';
import 'package:owners_yacht/widgets/ticket_type_card.dart';

class TicketType extends StatelessWidget {
  TicketTypeController _controller = Get.find<TicketTypeController>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const NavBar(title: 'Loại Vé', automaticallyImplyLeading: false),
      backgroundColor: Colors.grey[200],
      body: GetBuilder<TicketTypeController>(
        builder: (controller) => (_controller.isLoading.isTrue)
            ? const Center(child: CircularProgressIndicator())
            : controller.listTicketType.isEmpty
                ? const Center(child: Text('Không có loại vé nào!'))
                : Padding(
                    padding: const EdgeInsets.only(top: 10.0),
                    child: ListView.builder(
                      itemBuilder: (ctx, i) =>
                          TicketTypeCard(_controller.listTicketType[i]),
                      itemCount: _controller.listTicketType.length,
                    ),
                  ),
      ),
      floatingActionButton: FloatingActionButton(
        tooltip: 'Add',
        child: const Icon(Icons.add, color: Colors.black),
        onPressed: () => _controller.addTicketType(),
        backgroundColor: Colors.white,
      ),
    );
  }
}
