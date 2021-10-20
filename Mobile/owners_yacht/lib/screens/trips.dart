import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'dart:math';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/trip.dart';

import 'package:owners_yacht/widgets/nav_bar.dart';
import 'package:owners_yacht/widgets/trip_card.dart';
class Trips extends StatefulWidget {
  @override
  State<Trips> createState() => _TripsState();
}

class _TripsState extends State<Trips> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const NavBar(
        title: 'Chuyến đi',
        automaticallyImplyLeading: false,
      ),
      body: GetBuilder<TripController>(
        builder: (controller) => (controller.isLoading.isTrue)
            ? const Center(child: CircularProgressIndicator())
            : Padding(
                padding: const EdgeInsets.only(top: 10.0),
                child: ListView.builder(
                  itemBuilder: (ctx, i) => TripCard(controller.listTrip[i]),
                  itemCount: controller.listTrip.length,
                ),
              ),
      ),
    );
  }
}
//       // ListView.builder(
//       // itemCount: transactions.length,
//       // itemBuilder: (context, index) {
//       //   Transaction1 transaction = transactions[index];
//       //   DateTime date =
//       //       DateTime.fromMillisecondsSinceEpoch(transaction.createdMillis);
//       //   String dateString = DateFormat("EEE, MMM d, y").format(date);

//       //   if (today == dateString) {
//       //     dateString = "Hôm nay";
//       //   } else if (yesterday == dateString) {
//       //     dateString = "Hôm qua";
//       //   }

//       //   bool showHeader = prevDay != dateString;
//       //   prevDay = dateString;
//       //   return Column(
//       //     crossAxisAlignment: CrossAxisAlignment.start,
//       //     children: <Widget>[
//       //       showHeader
//       //           ? Container(
//       //               padding: EdgeInsets.symmetric(horizontal: 16, vertical: 16),
//       //               child: Text(
//       //                 dateString,
//       //                 style: TextStyle(
//       //                     color: Colors.black, fontWeight: FontWeight.bold),
//       //               ),
//       //             )
//       //           : Offstage(),
//       //       buildItem(index, context, date, transaction),
//       //     ],
//       //   );
//       // ),
//     );
//   }

//   ListView buildListView() {
//     return 
//   }

//   Widget buildItem(int index, BuildContext context, DateTime date,
//       Transaction1 transaction) {
//     return IntrinsicHeight(
//       child: Row(
//         crossAxisAlignment: CrossAxisAlignment.stretch,
//         children: <Widget>[
//           SizedBox(width: 20),
//           buildLine(index, context),
//           Container(
//             alignment: Alignment.centerLeft,
//             padding: EdgeInsets.symmetric(horizontal: 12, vertical: 16),
//             // color: Theme.of(context).accentColor,
//             child: Text(
//               DateFormat("hh:mm a").format(date),
//               style: TextStyle(
//                 // color: Colors.white,
//                 fontWeight: FontWeight.bold,
//               ),
//             ),
//           ),
//           Expanded(
//             flex: 1,
//             child: buildItemInfo(transaction, context),
//           ),
//         ],
//       ),
//     );
//   }

//   Card buildItemInfo(Transaction1 transaction, BuildContext context) {
//     return Card(
//       clipBehavior: Clip.antiAliasWithSaveLayer,
//       child: Container(
//         decoration: BoxDecoration(
//           gradient: LinearGradient(
//               colors: transaction.point.isNegative
//                   ? [Colors.deepOrange, Colors.red]
//                   : [Colors.green, Colors.teal]),
//         ),
//         child: Row(
//           children: <Widget>[
//             Expanded(
//               flex: 1,
//               child: Container(
//                 padding: EdgeInsets.symmetric(horizontal: 16, vertical: 16),
//                 child: Text(
//                   transaction.name,
//                   style: TextStyle(
//                       color: Colors.white, fontWeight: FontWeight.bold),
//                 ),
//               ),
//             ),
//             Container(
//               padding: EdgeInsets.symmetric(horizontal: 16, vertical: 16),
//               child: Text(
//                 NumberFormat("###,###,####k VND").format(transaction.point),
//                 style:
//                     TextStyle(color: Colors.white, fontWeight: FontWeight.bold),
//               ),
//             ),
//           ],
//         ),
//       ),
//     );
//   }

//   Container buildLine(int index, BuildContext context) {
//     return Container(
//       child: Column(
//         mainAxisSize: MainAxisSize.max,
//         children: <Widget>[
//           Expanded(
//             flex: 1,
//             child: Container(
//               width: 2,
//               color: Theme.of(context).accentColor,
//             ),
//           ),
//           Container(
//             width: 6,
//             height: 6,
//             decoration: BoxDecoration(
//                 color: Theme.of(context).accentColor, shape: BoxShape.circle),
//           ),
//           Expanded(
//             flex: 1,
//             child: Container(
//               width: 2,
//               color: Theme.of(context).accentColor,
//             ),
//           ),
//         ],
//       ),
//     );
//   }
// }
