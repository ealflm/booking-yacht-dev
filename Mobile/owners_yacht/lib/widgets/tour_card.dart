import 'dart:ui';

import 'package:flutter/material.dart';
import 'package:owners_yacht/constants/status.dart';
import 'package:owners_yacht/constants/theme.dart';
import 'package:owners_yacht/controller/tour.dart';
import 'package:owners_yacht/models/tour.dart';
import 'package:get/get.dart';

class TourCard extends StatelessWidget {
  final Tour tour;

  TourCard({Key? key, required this.tour}) : super(key: key);
  final TourController _tourController = Get.find<TourController>();
  @override
  Widget build(BuildContext context) {
    return Container(
        height: 130,
        margin: EdgeInsets.only(top: 10),
        child: GestureDetector(
          onTap: () => _tourController.getTourDetail(tour.id!),
          child: Stack(overflow: Overflow.clip, children: [
            Card(
              elevation: 0.7,
              shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.all(Radius.circular(6.0))),
              child: Row(
                children: [
                  Flexible(flex: 1, child: Container()),
                  Flexible(
                      flex: 1,
                      child: Padding(
                        padding: const EdgeInsets.only(top: 8.0, bottom: 8.0),
                        child: Column(
                          mainAxisAlignment: MainAxisAlignment.start,
                          crossAxisAlignment: CrossAxisAlignment.start,
                          children: [
                            Flexible(
                              child: Text('Tên: ${tour.title!}',
                                  overflow: TextOverflow.ellipsis,
                                  maxLines: 4,
                                  style: const TextStyle(
                                      color: BookingYachtColors.caption,
                                      fontSize: 16)),
                            ),
                            Text(
                                'Trạng thái: ${BookingYachtStatus.status[tour.status!]}',
                                style: const TextStyle(
                                    color: BookingYachtColors.muted,
                                    fontSize: 14,
                                    fontWeight: FontWeight.w600))
                          ],
                        ),
                      ))
                ],
              ),
            ),
            FractionalTranslation(
              translation: Offset(0.0, -0.09),
              child: Align(
                alignment: Alignment.centerLeft,
                child: Padding(
                  padding: const EdgeInsets.only(
                      top: 0.0, bottom: 0, left: 13, right: 13),
                  child: Container(
                      padding: const EdgeInsets.only(
                          left: 16.0, right: 16, bottom: 0, top: 16),
                      height: 127,
                      width: MediaQuery.of(context).size.width / 2.5,
                      decoration: BoxDecoration(
                          boxShadow: [
                            BoxShadow(
                                color: Colors.black.withOpacity(0.06),
                                spreadRadius: 2,
                                blurRadius: 1,
                                offset: const Offset(0, 0))
                          ],
                          borderRadius:
                              const BorderRadius.all(Radius.circular(4.0)),
                          image: DecorationImage(
                              image: NetworkImage(tour.imageLink ??
                                  'https://www.publicdomainpictures.net/pictures/280000/velka/not-found-image-15383864787lu.jpg'),
                              fit: BoxFit.cover))),
                ),
              ),
            ),
          ]),
        ));
  }
}
