import 'package:flutter/material.dart';
import 'package:owners_yacht/constants/Status.dart';
import 'package:owners_yacht/controller/yacht.dart';
import 'package:owners_yacht/models/yacht.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/screens/yacht_detail.dart';

import 'information.dart';

class YachtCard extends StatelessWidget {
  YachtCard(this.yacht);

  final Yacht yacht;
  final YachtController controller = Get.find<YachtController>();
  @override
  Widget build(BuildContext context) {
    return Container(
      height: 250,
      width: null,
      margin: const EdgeInsets.all(10),
      child: GestureDetector(
        onTap: () => controller.getYachtDetail(yacht.id!),
        child: Stack(
          overflow: Overflow.clip,
          children: [
            Card(
              elevation: 0.7,
              shape: const RoundedRectangleBorder(
                  borderRadius: BorderRadius.all(Radius.circular(8.0))),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Flexible(flex: 3, child: Container()),
                  Flexible(
                    flex: 2,
                    child: Padding(
                      padding: const EdgeInsets.only(
                          top: 1.0, bottom: 5.0, left: 10.0),
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          YachtInformation('Tên tàu', yacht.name.toString()),
                          YachtInformation(
                              'Trạng thái',
                              BookingYachtStatus.status[yacht.status]
                                  .toString()),
                          YachtInformation('Ghế', yacht.seat.toString()),
                          YachtInformation('Loại tàu',
                              yacht.yearOfManufacture.toString()),
                        ],
                      ),
                    ),
                  ),
                ],
              ),
            ),
            FractionalTranslation(
              translation: Offset(0, -0.05),
              child: Align(
                alignment: Alignment.topCenter,
                child: Container(
                  padding: const EdgeInsets.all(4.0),
                  height: 150,
                  width: MediaQuery.of(context).size.width * 0.83,
                  decoration: BoxDecoration(
                    boxShadow: [
                      BoxShadow(
                          color: Colors.black.withOpacity(0.06),
                          spreadRadius: 2,
                          blurRadius: 1,
                          offset: Offset(0, 0))
                    ],
                    borderRadius: BorderRadius.all(Radius.circular(4.0)),
                    image: DecorationImage(
                      image: NetworkImage(yacht.imageLink ??
                          'https://www.publicdomainpictures.net/pictures/280000/velka/not-found-image-15383864787lu.jpg'),
                      fit: BoxFit.fill,
                    ),
                  ),
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}
