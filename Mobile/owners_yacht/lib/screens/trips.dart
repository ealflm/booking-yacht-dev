import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/trip.dart';

import 'package:owners_yacht/widgets/nav_bar.dart';
import 'package:owners_yacht/widgets/trip_card.dart';
import 'package:table_calendar/table_calendar.dart';
import 'package:intl/intl.dart';
import 'package:intl/date_symbol_data_local.dart';

class Trips extends StatefulWidget {
  @override
  State<Trips> createState() => _TripsState();
}

class _TripsState extends State<Trips> {
  CalendarFormat _calendarFormat = CalendarFormat.week;
  DateTime _focusedDay = DateTime.now();
  DateTime? _selectedDay;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const NavBar(
        title: 'Trang chủ',
        automaticallyImplyLeading: false,
      ),
      body: Column(
        children: [
          TableCalendar(
            locale: 'vi',
            availableCalendarFormats: const {
              CalendarFormat.month: 'Tháng',
              CalendarFormat.twoWeeks: '2 tuần',
              CalendarFormat.week: 'Tuần'
            },
            firstDay: DateTime.utc(2020, 1, 1),
            lastDay: DateTime.utc(2025, 1, 1),
            focusedDay: _focusedDay,
            calendarFormat: _calendarFormat,
            selectedDayPredicate: (day) {
              // Use `selectedDayPredicate` to determine which day is currently selected.
              // If this returns true, then `day` will be marked as selected.

              // Using `isSameDay` is recommended to disregard
              // the time-part of compared DateTime objects.
              return isSameDay(_selectedDay, day);
            },
            onDaySelected: (selectedDay, focusedDay) {
              if (!isSameDay(_selectedDay, selectedDay)) {
                // Call `setState()` when updating the selected day
                setState(() {
                  print(focusedDay);
                  _selectedDay = selectedDay;
                  _focusedDay = focusedDay;
                });
              }
            },
            onFormatChanged: (format) {
              if (_calendarFormat != format) {
                // Call `setState()` when updating calendar format
                setState(() {
                  _calendarFormat = format;
                });
              }
            },
            onPageChanged: (focusedDay) {
              // No need to call `setState()` here
              _focusedDay = focusedDay;
            },
          ),
          Flexible(
            child: SizedBox(
              child: GetBuilder<TripController>(
                builder: (controller) => (controller.isLoading.isTrue)
                    ? const Center(child: CircularProgressIndicator())
                    : controller.listBusinessTour.isEmpty
                        ? const Center(child: Text('Không có tàu nào!'))
                        : Padding(
                            padding: const EdgeInsets.only(top: 10.0),
                            child: ListView.builder(
                              shrinkWrap: true,
                              itemBuilder: (ctx, i) => Padding(
                                padding: const EdgeInsets.all(8.0),
                                child: Card(
                                  elevation: 3,
                                  color: Colors.grey[200],
                                  child: ExpansionTile(
                                    title: Text(
                                      'Tour: ${controller.listBusinessTour[i].idTourNavigation!.title}',
                                      style: const TextStyle(
                                          fontWeight: FontWeight.w600),
                                    ),
                                    children: [
                                      ListView.builder(
                                        shrinkWrap: true,
                                        itemBuilder: (ctx, i) => Column(
                                          children: [
                                            ExpansionTile(
                                              title: Text(
                                                  'Time: ${DateFormat('hh:mm a', 'vi-VN').format(DateTime.now())}'),
                                              subtitle: Column(
                                                mainAxisAlignment:
                                                    MainAxisAlignment.start,
                                                crossAxisAlignment:
                                                    CrossAxisAlignment.start,
                                                children: [
                                                  Text('Số lượng người đi: 1'),
                                                  Text('Trạng thái: Đang đi'),
                                                ],
                                              ),
                                              children: [
                                                Text('Tàu: vip'),
                                                Text('Số ghế: 36'),
                                              ],
                                            ),
                                          ],
                                        ),
                                        itemCount: 2,
                                      ),
                                    ],
                                  ),
                                ),
                              ),
                              itemCount: controller.listBusinessTour.length,
                            ),
                          ),
              ),
            ),
          ),
        ],
      ),
    );
  }
}

      //  GetBuilder<TripController>(
      //   builder: (controller) => (controller.isLoading.isTrue)
      //       ? const Center(child: CircularProgressIndicator())
      //       : controller.listTrip.isEmpty
      //           ? const Center(child: Text('Không có chuyến đi nào!'))
      //           : Padding(
      //               padding: const EdgeInsets.only(top: 10.0),
      //               child: ListView.builder(
      //                 itemBuilder: (ctx, i) => TripCard(controller.listTrip[i]),
      //                 itemCount: controller.listTrip.length,
      //               ),
      //             ),
      // ),
//     );
//   }
// }
















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
