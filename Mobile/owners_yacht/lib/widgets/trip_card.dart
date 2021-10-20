import 'package:flutter/material.dart';
import 'package:intl/intl.dart';
import 'package:owners_yacht/constants/Status.dart';
import 'package:owners_yacht/models/trip.dart';

// import 'dart:math';
class TripCard extends StatelessWidget {
  final Trip _trip;
  final String _today = DateFormat("EEE, MMM d, y").format(DateTime.now());
  final String _yesterday = DateFormat("EEE, MMM d, y")
      .format(DateTime.now().add(const Duration(days: -1)));
  TripCard(this._trip);
  @override
  Widget build(BuildContext context) {
    String dateString = DateFormat("EEE, MMM d, y").format(_trip.time!);

    if (_today == dateString) {
      dateString = "Hôm nay";
    } else if (_yesterday == dateString) {
      dateString = "Hôm qua";
    }
    return Column(
      crossAxisAlignment: CrossAxisAlignment.start,
      children: <Widget>[
        // showHeader
        // ?
        Container(
          padding: EdgeInsets.symmetric(horizontal: 16, vertical: 16),
          child: Text(
            '${dateString}',
            style: TextStyle(color: Colors.black, fontWeight: FontWeight.bold),
          ),
        ),
        // :
        // Offstage(),

        IntrinsicHeight(
          child: Row(
            crossAxisAlignment: CrossAxisAlignment.stretch,
            children: <Widget>[
              SizedBox(width: 20),
              Container(
                child: Column(
                  mainAxisSize: MainAxisSize.max,
                  children: <Widget>[
                    Expanded(
                      flex: 1,
                      child: Container(
                        width: 2,
                        color: Theme.of(context).accentColor,
                      ),
                    ),
                    Container(
                      width: 6,
                      height: 6,
                      decoration: BoxDecoration(
                          color: Theme.of(context).accentColor,
                          shape: BoxShape.circle),
                    ),
                    Expanded(
                      flex: 1,
                      child: Container(
                        width: 2,
                        color: Theme.of(context).accentColor,
                      ),
                    ),
                  ],
                ),
              ),
              Container(
                alignment: Alignment.centerLeft,
                padding: EdgeInsets.symmetric(horizontal: 12, vertical: 16),
                // color: Theme.of(context).accentColor,
                child: Text(
                  DateFormat("hh:mm a").format(_trip.time!),
                  style: const TextStyle(
                    // color: Colors.white,
                    fontWeight: FontWeight.bold,
                  ),
                ),
              ),
              Expanded(
                flex: 1,
                child: Card(
                  clipBehavior: Clip.antiAliasWithSaveLayer,
                  child: Container(
                    decoration: BoxDecoration(
                      gradient: LinearGradient(
                          colors: _trip.status == 2
                              ? [Colors.deepOrange, Colors.red]
                              : [Colors.green, Colors.teal]),
                    ),
                    child: Row(
                      children: <Widget>[
                        Expanded(
                          flex: 1,
                          child: Container(
                            padding: const EdgeInsets.symmetric(
                                horizontal: 16, vertical: 16),
                            child: Text(
                              _trip.idVehicle!,
                              style: const TextStyle(
                                  color: Colors.white,
                                  fontWeight: FontWeight.bold),
                            ),
                          ),
                        ),
                        Container(
                          padding: const EdgeInsets.symmetric(
                              horizontal: 16, vertical: 16),
                          child: Text(
                            '${BookingYachtStatus.status[_trip.status]}',
                            style: const TextStyle(
                                color: Colors.white,
                                fontWeight: FontWeight.bold),
                          ),
                        ),
                      ],
                    ),
                  ),
                ),
              ),
            ],
          ),
        )
      ],
    );
  }
}
