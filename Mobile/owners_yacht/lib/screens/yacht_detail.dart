// ignore_for_file: unnecessary_string_interpolations

import 'package:flutter/material.dart';
import 'package:owners_yacht/constants/Status.dart';
import '/controller/yacht.dart';
import '/widgets/information.dart';
import 'package:get/get.dart';

class YachtDetail extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return GetBuilder<YachtController>(
      builder: (controller) => Scaffold(
        appBar: AppBar(
          title: Text(
            controller.yachtDetail.name!,
            style: TextStyle(color: Colors.white),
          ),
          backgroundColor: Colors.black,
          actions: [
            IconButton(
              icon: Icon(
                Icons.edit,
                color: Colors.white,
              ),
              onPressed: () => controller.editYacht(controller.yachtDetail),
            ),
          ],
        ),
        body: SingleChildScrollView(
          child: Column(
            children: [
              Padding(
                padding: EdgeInsets.symmetric(horizontal: 16.0),
                child: Container(
                  decoration: BoxDecoration(
                    boxShadow: [
                      BoxShadow(
                          color: Colors.black.withOpacity(0.2),
                          spreadRadius: 8,
                          blurRadius: 10,
                          offset: Offset(0, 0))
                    ],
                    color: Colors.white,
                    borderRadius: BorderRadius.all(Radius.circular(13)),
                  ),
                  margin: EdgeInsets.only(
                    top: MediaQuery.of(context).size.height * 0.04,
                    bottom: MediaQuery.of(context).size.height * 0.0001,
                  ),
                  alignment: Alignment.bottomCenter,
                  child: Stack(
                    children: [
                      Padding(
                        padding:
                            EdgeInsets.symmetric(horizontal: 18, vertical: 12),
                        child: SafeArea(
                          bottom: true,
                          top: false,
                          child: Padding(
                            padding: const EdgeInsets.only(
                                top: 1.0, bottom: 1.0, left: 10.0),
                            child: Column(
                              mainAxisAlignment: MainAxisAlignment.start,
                              crossAxisAlignment: CrossAxisAlignment.stretch,
                              children: [
                                Image.network(controller
                                        .yachtDetail.imageLink ??
                                    'https://www.publicdomainpictures.net/pictures/280000/velka/not-found-image-15383864787lu.jpg'),
                                Padding(
                                  padding: const EdgeInsets.only(top: 8.0),
                                  child: Column(
                                    crossAxisAlignment:
                                        CrossAxisAlignment.stretch,
                                    children: [
                                      YachtInformation(
                                          "Trạng thái",
                                          BookingYachtStatus.status[
                                                  controller.yachtDetail.status]
                                              .toString()),
                                      YachtInformation("Ghế",
                                          '${controller.yachtDetail.seat.toString()}'),
                                      YachtInformation("Biển số",
                                          '${controller.yachtDetail.registrationNumber.toString()}'),
                                      YachtInformation("Năm sản xuất",
                                          '${controller.yachtDetail.yearOfManufacture.toString()}'),
                                      YachtInformation("Nơi sản xuất",
                                          '${controller.yachtDetail.whereProduction.toString()}'),
                                      YachtInformation("Mô tả",
                                          controller.yachtDetail.descriptions!),
                                    ],
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
              FlatButton(
                onPressed: () => showDialog(
                  context: context,
                  builder: (ctx) => AlertDialog(
                    title: Text(controller.yachtDetail.status == 1
                        ? 'Xoá tàu?'
                        : 'Khôi phục'),
                    content: Text(
                      controller.yachtDetail.status == 1
                          ? 'Bạn có muốn xoá tàu này?'
                          : 'Bạn có muốn khôi phục này?',
                    ),
                    actions: <Widget>[
                      FlatButton(
                        child: const Text('Không'),
                        onPressed: () => controller.cancel(),
                      ),
                      FlatButton(
                        child: const Text('Có'),
                        onPressed: () => controller.yachtDetail.status == 1
                            ? controller.deleteYacht(controller.yachtDetail.id!)
                            : controller.restoreYacht(),
                      ),
                    ],
                  ),
                ),
                child: Text(controller.yachtDetail.status == 1 ?
                  'Xoá tàu' : 'Khôi phục',
                  style: const TextStyle(fontWeight: FontWeight.w300),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
