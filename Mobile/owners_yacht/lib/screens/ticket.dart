import 'package:flutter/material.dart';
import 'package:owners_yacht/constants/status.dart';
import 'package:owners_yacht/constants/theme.dart';
import 'package:owners_yacht/controller/ticket.dart';
import 'package:intl/intl.dart';
import 'package:owners_yacht/widgets/nav_bar.dart';
import 'package:get/get.dart';

class Tickets extends StatelessWidget {
  TicketController _ticketController = Get.find<TicketController>();

  Tickets({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: NavBar(
        title: ('Vé'),
        color: BookingYachtColors.appBar,
      ),
      body: Card(
        elevation: 3,
        child: Padding(
          padding:
              const EdgeInsets.only(left: 16, right: 16, top: 8, bottom: 8),
          child: Column(
            mainAxisSize: MainAxisSize.min,
            children: <Widget>[
              Container(
                padding: const EdgeInsets.all(16),
                decoration: const BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.only(
                        topLeft: Radius.circular(24),
                        topRight: Radius.circular(24))),
                child: Column(
                  children: <Widget>[
                    Row(
                      mainAxisAlignment: MainAxisAlignment.start,
                      children: <Widget>[
                        const Text(
                          "Tên: ",
                          style: TextStyle(
                              fontSize: 18,
                              fontWeight: FontWeight.bold,
                              color: Colors.indigo),
                        ),
                        SizedBox(
                          width: 16,
                        ),
                        SizedBox(
                          width: 16,
                        ),
                        Text(
                          "${_ticketController.ticketDetail.nameCustomer}",
                          style: const TextStyle(
                              fontSize: 18,
                              fontWeight: FontWeight.bold,
                              color: Colors.black),
                        )
                      ],
                    ),
                    const SizedBox(
                      height: 4,
                    ),
                    const SizedBox(
                      height: 12,
                    ),
                    Row(
                      mainAxisAlignment: MainAxisAlignment.spaceBetween,
                      children: <Widget>[
                        Flexible(
                          child: Column(
                            mainAxisAlignment: MainAxisAlignment.start,
                            crossAxisAlignment: CrossAxisAlignment.stretch,
                            children: [
                              Text(
                                  "Số điện thoại: ${_ticketController.ticketDetail.phone}",
                                  style: const TextStyle(
                                      fontSize: 12, color: Colors.grey)),
                              Text(
                                  "Trạng thái: ${BookingYachtStatus.status[_ticketController.ticketDetail.status]}",
                                  style: const TextStyle(
                                      fontSize: 12, color: Colors.grey)),
                            ],
                          ),
                        ),
                      ],
                    ),
                  ],
                ),
              ),
              Container(
                color: Colors.white,
                child: Row(
                  children: <Widget>[
                    SizedBox(
                      height: 20,
                      width: 10,
                      child: DecoratedBox(
                        decoration: BoxDecoration(
                            borderRadius: const BorderRadius.only(
                                topRight: Radius.circular(10),
                                bottomRight: Radius.circular(10)),
                            color: Colors.grey.shade200),
                      ),
                    ),
                    Expanded(
                      child: Padding(
                        padding: const EdgeInsets.all(8.0),
                        child: LayoutBuilder(
                          builder: (context, constraints) {
                            return Flex(
                              children: List.generate(
                                  (constraints.constrainWidth() / 10).floor(),
                                  (index) => SizedBox(
                                        height: 1,
                                        width: 5,
                                        child: DecoratedBox(
                                          decoration: BoxDecoration(
                                              color: Colors.grey.shade400),
                                        ),
                                      )),
                              direction: Axis.horizontal,
                              mainAxisSize: MainAxisSize.max,
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            );
                          },
                        ),
                      ),
                    ),
                    SizedBox(
                      height: 20,
                      width: 10,
                      child: DecoratedBox(
                        decoration: BoxDecoration(
                            borderRadius: const BorderRadius.only(
                                topLeft: Radius.circular(10),
                                bottomLeft: Radius.circular(10)),
                            color: Colors.grey.shade200),
                      ),
                    ),
                  ],
                ),
              ),
              Container(
                padding: const EdgeInsets.only(left: 16, right: 16, bottom: 12),
                decoration: const BoxDecoration(
                    color: Colors.white,
                    borderRadius: BorderRadius.only(
                        bottomLeft: Radius.circular(24),
                        bottomRight: Radius.circular(24))),
                child: Row(
                  children: <Widget>[
                    const SizedBox(
                      width: 16,
                    ),
                    const Text("Giá vé",
                        style: TextStyle(
                            fontSize: 16,
                            fontWeight: FontWeight.w500,
                            color: Colors.grey)),
                    Expanded(
                        child: Text(
                            "${NumberFormat.currency(locale: "vi-VN", symbol: "VND").format(_ticketController.ticketDetail.price)}",
                            textAlign: TextAlign.end,
                            style: const TextStyle(
                                fontSize: 18,
                                fontWeight: FontWeight.bold,
                                color: Colors.black))),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
