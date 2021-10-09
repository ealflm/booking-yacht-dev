import 'package:flutter/material.dart';
import 'package:owners_yacht/controller/yacht.dart';
import 'package:owners_yacht/models/yacht.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/screens/yacht_detail.dart';

class YachtCard extends StatelessWidget {
  YachtCard(this.yacht);

  final Yacht yacht;
  final YachtController controller = Get.find<YachtController>();
  @override
  Widget build(BuildContext context) {
    return Container(
        height: 250,
        width: null,
        margin: EdgeInsets.all(10),
        child: GestureDetector(
            onTap: () => controller.getYacht(yacht.id!),
            child: Stack(
              overflow: Overflow.clip,
              children: [
                Card(
                    elevation: 0.7,
                    shape: RoundedRectangleBorder(
                        borderRadius: BorderRadius.all(Radius.circular(8.0))),
                    child: Column(
                      crossAxisAlignment: CrossAxisAlignment.start,
                      children: [
                        Flexible(flex: 3, child: Container()),
                        Flexible(
                            flex: 1,
                            child: Padding(
                              padding: const EdgeInsets.only(
                                  top: 8.0, bottom: 8.0, left: 8.0),
                              child: Column(
                                mainAxisAlignment:
                                    MainAxisAlignment.spaceBetween,
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text('Tên tàu: ${yacht.name}',
                                      style: TextStyle(
                                          color: Colors.black, fontSize: 16)),
                                  Text('Trạng thái: ${yacht.status}',
                                      style: TextStyle(
                                          color: Colors.black,
                                          fontSize: 11,
                                          fontWeight: FontWeight.w600))
                                ],
                              ),
                            ))
                      ],
                    )),
                FractionalTranslation(
                  translation: Offset(0, -0.05),
                  child: Align(
                    alignment: Alignment.topCenter,
                    child: Container(
                      padding: EdgeInsets.all(4.0),
                      height: 197,
                      width: MediaQuery.of(context).size.width * 0.85,
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
                          image: NetworkImage(
                              "https://i1.wp.com/www.barcheamotore.com/wp-content/uploads/2019/10/Ferretti-Yachts-720_1.jpg?fit=900%2C500&ssl=1"),
                          fit: BoxFit.cover,
                        ),
                      ),
                    ),
                  ),
                )
              ],
            )));
  }
}
