import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/constants/status.dart';
import 'package:owners_yacht/constants/theme.dart';
import 'package:owners_yacht/controller/trip.dart';
import 'package:owners_yacht/controller/yacht.dart';
import 'package:owners_yacht/models/yacht.dart';

import 'package:owners_yacht/widgets/nav_bar.dart';
import 'package:owners_yacht/widgets/trip_card.dart';
import 'package:table_calendar/table_calendar.dart';
import 'package:intl/intl.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'package:flutter_datetime_picker/flutter_datetime_picker.dart';

class Trips extends StatefulWidget {
  @override
  State<Trips> createState() => _TripsState();
}

class _TripsState extends State<Trips> {
  TripController _tripController = Get.find<TripController>();
  CalendarFormat _calendarFormat = CalendarFormat.week;
  DateTime _focusedDay = DateTime.now();
  DateTime? _selectedDay;

  @override
  initState() {
    super.initState();
    // Add listeners to this class
    _tripController.focusedDay = _focusedDay;
  }

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
            startingDayOfWeek: StartingDayOfWeek.monday,
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
                  _tripController.focusedDay = focusedDay;
                  _tripController.getBusinessTour();
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
                        ? const Center(child: Text('Không có chuyến đi!'))
                        : Padding(
                            padding: const EdgeInsets.only(top: 10.0),
                            child: ListView.builder(
                              shrinkWrap: true,
                              itemBuilder: (ctx, index) => Padding(
                                padding: const EdgeInsets.all(8.0),
                                child: Card(
                                  elevation: 3,
                                  child: ExpansionTile(
                                    title: ListTile(
                                      title: Text(
                                          '${controller.listBusinessTour[index].idTourNavigation!.title}'),
                                    ),
                                    children: [
                                      Column(
                                        children: [
                                          ListView.builder(
                                            shrinkWrap: true,
                                            itemBuilder: (ctx, i) => Padding(
                                              padding:
                                                  const EdgeInsets.symmetric(
                                                      horizontal: 30.0),
                                              child: Column(
                                                children: [
                                                  ExpansionTile(
                                                    title: ListTile(
                                                      title: Text(
                                                          'Chuyến đi lúc: ${DateFormat('hh:mm a', 'vi-VN').format(controller.listBusinessTour[index].trips![i].time ?? DateTime.now())}'),
                                                      subtitle: Column(
                                                        children: [
                                                          ListTile(
                                                              title: Text(
                                                                  'Số người đi: ${controller.listBusinessTour[index].trips![i].amountTicket}'),
                                                              subtitle: Text(
                                                                  'Trạng thái: ${BookingYachtStatus.status[controller.listBusinessTour[index].trips![i].status]}'))
                                                        ],
                                                      ),
                                                    ),
                                                    children: [
                                                      Padding(
                                                        padding:
                                                            const EdgeInsets
                                                                    .symmetric(
                                                                horizontal:
                                                                    30.0),
                                                        child: ListTile(
                                                            title: Text(
                                                                'Tàu ${controller.listBusinessTour[index].trips![i].idVehicleNavigation!.name}'),
                                                            subtitle: Text(
                                                                'Số ghế: ${controller.listBusinessTour[index].trips![i].idVehicleNavigation!.seat}')),
                                                      )
                                                    ],
                                                  ),
                                                ],
                                              ),
                                            ),
                                            itemCount: controller
                                                    .listBusinessTour[index]
                                                    .trips!
                                                    .isEmpty
                                                ? 0
                                                : controller
                                                    .listBusinessTour[index]
                                                    .trips!
                                                    .length,
                                          ),
                                          TextButton(
                                            onPressed: () => showModalBottomSheet<
                                                    void>(
                                                shape:
                                                    const RoundedRectangleBorder(
                                                  borderRadius:
                                                      BorderRadius.only(
                                                          topLeft:
                                                              Radius.circular(
                                                                  20.0),
                                                          topRight:
                                                              Radius.circular(
                                                                  20.0)),
                                                ),
                                                context: context,
                                                builder:
                                                    (BuildContext context) {
                                                  return SingleChildScrollView(
                                                    child: Container(
                                                      height:
                                                          MediaQuery.of(context)
                                                                  .size
                                                                  .height /
                                                              2,
                                                      child: Padding(
                                                        padding:
                                                            const EdgeInsets
                                                                .all(8.0),
                                                        child: Column(
                                                          mainAxisAlignment:
                                                              MainAxisAlignment
                                                                  .start,
                                                          crossAxisAlignment:
                                                              CrossAxisAlignment
                                                                  .start,
                                                          children: <Widget>[
                                                            const Center(
                                                                child: Text(
                                                              'Chọn tàu',
                                                              style: TextStyle(
                                                                  fontWeight:
                                                                      FontWeight
                                                                          .w600,
                                                                  fontSize: 14),
                                                            )),
                                                            GetBuilder<
                                                                YachtController>(
                                                              builder:
                                                                  (_yachtController) =>
                                                                      Padding(
                                                                padding:
                                                                    const EdgeInsets
                                                                            .all(
                                                                        5.0),
                                                                child: Wrap(
                                                                  children: [
                                                                    DropdownButton<
                                                                        String>(
                                                                      isExpanded:
                                                                          true,
                                                                      hint:
                                                                          const Text(
                                                                        'Chọn tàu',
                                                                        style: TextStyle(
                                                                            fontSize:
                                                                                16),
                                                                      ),
                                                                      value: controller
                                                                          .selectedYacht,
                                                                      items: _yachtController
                                                                          .listYacht
                                                                          .map((Yacht
                                                                              value) {
                                                                        return DropdownMenuItem<
                                                                            String>(
                                                                          value:
                                                                              value.id,
                                                                          child: Text(
                                                                              value.name!,
                                                                              overflow: TextOverflow.ellipsis),
                                                                        );
                                                                      }).toList(),
                                                                      onChanged:
                                                                          (String?
                                                                              newValue) {
                                                                        setState(
                                                                            () {
                                                                          controller.selectedYacht =
                                                                              newValue!;
                                                                        });
                                                                      },
                                                                    )
                                                                  ],
                                                                ),
                                                              ),
                                                            ),
                                                            const Center(
                                                                child: Text(
                                                              'Tổng vé',
                                                              style: TextStyle(
                                                                  fontWeight:
                                                                      FontWeight
                                                                          .w600,
                                                                  fontSize: 14),
                                                            )),
                                                            TextFormField(
                                                              decoration:
                                                                  const InputDecoration(
                                                                      labelText:
                                                                          'Số lượng vé'),
                                                              textInputAction:
                                                                  TextInputAction
                                                                      .next,
                                                              keyboardType:
                                                                  TextInputType
                                                                      .number,
                                                              controller: controller
                                                                  .amountTicket,
                                                              onSaved: (value) {
                                                                controller
                                                                    .amountTicket
                                                                    .text = value!;
                                                              },
                                                            ),
                                                            Divider(),
                                                            const Center(
                                                                child: Text(
                                                              'Thời gian',
                                                              style: TextStyle(
                                                                  fontWeight:
                                                                      FontWeight
                                                                          .w600,
                                                                  fontSize: 14),
                                                            )),
                                                            TextButton(
                                                              onPressed: () {
                                                                DatePicker.showTimePicker(
                                                                    context,
                                                                    showTitleActions:
                                                                        true,
                                                                    onChanged:
                                                                        (date) {},
                                                                    onConfirm:
                                                                        (date) {
                                                                  _tripController
                                                                          .time =
                                                                      date;
                                                                },
                                                                    locale:
                                                                        LocaleType
                                                                            .vi);
                                                              },
                                                              child: Center(
                                                                child: Text(
                                                                  _tripController
                                                                              .time ==
                                                                          null
                                                                      ? 'Chọn thời gian'
                                                                      : ' ${DateFormat('hh:mm a', 'vi-VN').format(_tripController.time)}',
                                                                  style: TextStyle(
                                                                      color: Colors
                                                                          .blue),
                                                                ),
                                                              ),
                                                            ),
                                                            Padding(
                                                              padding:
                                                                  const EdgeInsets
                                                                          .only(
                                                                      top:
                                                                          10.0),
                                                              child: Center(
                                                                child:
                                                                    OutlinedButton(
                                                                  style: OutlinedButton
                                                                      .styleFrom(
                                                                    side: BorderSide(
                                                                        color: BookingYachtColors
                                                                            .appBar),
                                                                  ),
                                                                  onPressed: () =>
                                                                      _tripController.save(controller
                                                                          .listBusinessTour[
                                                                              index]
                                                                          .id!),
                                                                  child:
                                                                      const Text(
                                                                    "Xác nhận",
                                                                    style: TextStyle(
                                                                        color: BookingYachtColors
                                                                            .appBar),
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
                                            child: Text(
                                              'Thêm chuyến đi',
                                              style:
                                                  TextStyle(color: Colors.blue),
                                            ),
                                          ),
                                        ],
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
