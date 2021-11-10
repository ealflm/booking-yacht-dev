import 'package:flutter/material.dart';
import 'package:owners_yacht/constants/Status.dart';
import 'package:owners_yacht/controller/order.dart';
import 'package:owners_yacht/models/order.dart';
import 'package:flutter_slidable/flutter_slidable.dart';
import 'package:intl/intl.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:get/get.dart';

class OrderCard extends StatelessWidget {
  final Order order;
  final OrderController _orderController = Get.find<OrderController>();
  OrderCard({Key? key, required this.order}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return
        //  Slidable(
        // actionPane: SlidableDrawerActionPane(),
        // actionExtentRatio: 0.25,
        // child:
        GestureDetector(
      onTap: () => showModalBottomSheet<void>(
          shape: const RoundedRectangleBorder(
            borderRadius: BorderRadius.only(
                topLeft: Radius.circular(20.0),
                topRight: Radius.circular(20.0)),
          ),
          context: context,
          builder: (BuildContext context) {
            return SingleChildScrollView(
              child: Container(
                height: MediaQuery.of(context).size.height / 3,
                child: Padding(
                  padding: const EdgeInsets.all(8.0),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.start,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: <Widget>[
                      const Center(
                        child: Text('Loại vé',
                            style: TextStyle(
                                fontWeight: FontWeight.w600, fontSize: 14)),
                      ),
                      Text(
                          'Giá: ${NumberFormat.currency(locale: "vi-VN", symbol: "VND").format(order.totalPrice!)}'),
                      Divider(),
                      const Center(
                          child: Text(
                        'Thông tin đại lý',
                        style: TextStyle(
                            fontWeight: FontWeight.w600, fontSize: 14),
                      )),
                      Text('Tên đại lý: ${order.agencyName}'),
                      Text(
                          'Số điện thoại: ${order.agencyViewModels!.phoneNumber.toString()}'),
                      Text('Email: ${order.agencyViewModels!.emailAddress}'),
                      Text('Email: ${order.agencyViewModels!.address}'),
                      Divider(),
                      Padding(
                        padding: const EdgeInsets.only(top: 10.0),
                        child: Center(
                          child: OutlinedButton(
                            style: OutlinedButton.styleFrom(
                              side: BorderSide(color: Colors.redAccent),
                            ),
                            onPressed: () =>
                                _orderController.deleteOrder(order.id!),
                            child: const Text(
                              "Từ chối đơn",
                              style: TextStyle(color: Colors.redAccent),
                            ),
                          ),
                        ),
                      )
                    ],
                  ),
                ),
              ),
            );
          }),
      child: Card(
        elevation: 2,
        child: ListTile(
          leading: CircleAvatar(
            backgroundImage: NetworkImage(order.agencyViewModels!.photoUrl ??
                'https://cdn5.vectorstock.com/i/1000x1000/01/69/businesswoman-character-avatar-icon-vector-12800169.jpg'),
          ),
          title: Text('Tour: ${order.tourName}'),
          subtitle: Column(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                  'Giá: ${NumberFormat.currency(locale: "vi-VN", symbol: "VND").format(order.totalPrice)}'),
              Text(
                  'Trạng thái: ${BookingYachtStatus.status[order.status].toString()}'),
              Text('Số vé: ${order.quantityOfPerson}'),
              Text(
                  'Thời gian đặt: ${DateFormat('hh:mm a - dd MMMM yyyy', 'vi-VN').format(order.orderDate ?? DateTime.now())}'),
            ],
          ),
        ),
        // ),
        // secondaryActions: <Widget>[
        //   IconSlideAction(
        //     caption: 'Chấp nhận',
        //     color: Colors.green,
        //     icon: Icons.check,
        //     onTap: () => print('say hello'),
        //   ),
        //   IconSlideAction(
        //     caption: 'Xem thêm',
        //     color: Colors.black45,
        //     icon: Icons.more_horiz,
        //     onTap: () => print('hello'),
        //   ),
        // ],
      ),
    );
  }
}
