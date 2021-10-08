import 'package:flutter/material.dart';
import 'package:owners_yacht/models/yacht.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/screens/yacht_detail.dart';

class YachtCard extends StatelessWidget {
  YachtCard(this.yacht);

  final Yacht yacht;

  @override
  Widget build(BuildContext context) {
    return Flexible(
        child: Container(
      height: 230,
      margin: EdgeInsets.only(top: 10),
      child: GestureDetector(
          onTap: () {
            Get.to(YachtDetail(yacht));
          },
          child: Stack(overflow: Overflow.clip, children: [
            Card(
                elevation: 0.7,
                shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.all(Radius.circular(8.0))),
                child: Column(
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Flexible(flex: 2, child: Container()),
                    Flexible(
                        flex: 1,
                        child: Padding(
                          padding: const EdgeInsets.only(
                              top: 8.0, bottom: 8.0, left: 8.0),
                          child: Column(
                            mainAxisAlignment: MainAxisAlignment.spaceBetween,
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text('Tên: ${yacht.name}',
                                  style: TextStyle(
                                      color: Colors.black, fontSize: 13)),
                              Padding(
                                padding: const EdgeInsets.only(top: 8.0),
                                child: Text('Trạng thái: ${yacht.status}',
                                    style: TextStyle(
                                        color: Colors.green,
                                        fontSize: 11,
                                        fontWeight: FontWeight.w600)),
                              )
                            ],
                          ),
                        ))
                  ],
                )),
            FractionalTranslation(
                translation: Offset(0, 0),
                child: Align(
                    alignment: Alignment.topCenter,
                    child: Padding(
                      padding: const EdgeInsets.all(13.0),
                      child: Container(
                          height: 185,
                          padding: EdgeInsets.all(16.0),
                          decoration: BoxDecoration(
                              boxShadow: [
                                BoxShadow(
                                    color: Colors.black.withOpacity(0.06),
                                    spreadRadius: 2,
                                    blurRadius: 1,
                                    offset: Offset(0, 0))
                              ],
                              borderRadius:
                                  BorderRadius.all(Radius.circular(4.0)),
                              image: DecorationImage(
                                image: NetworkImage(
                                    'https://www.ferretti-yachts.com/uploadB2B/Models/Images/Main/Ferretti/medium/47591.jpg'),
                                fit: BoxFit.cover,
                              ))),
                    )))
          ])),
    ));
  }
}
