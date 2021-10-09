import 'package:flutter/material.dart';
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
            controller.yachtDetail[0].name,
            style: TextStyle(color: Colors.white),
          ),
          backgroundColor: Colors.black,
          actions: [
            IconButton(
              icon: Icon(
                Icons.edit,
                color: Colors.white,
              ),
              onPressed: () => controller.edit(),
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
                          child: Column(
                            mainAxisAlignment: MainAxisAlignment.spaceAround,
                            children: [
                              Image.network(
                                  'https://i1.wp.com/www.barcheamotore.com/wp-content/uploads/2019/10/Ferretti-Yachts-720_1.jpg?fit=900%2C500&ssl=1'),
                              YachtInformation(
                                  "Seat",
                                  (controller.yachtDetail[0].seat != null)
                                      ? '${controller.yachtDetail[0].seat}'
                                      : 'No'),
                              YachtInformation(
                                  "Status",
                                  (controller.yachtDetail[0].status != null)
                                      ? '${controller.yachtDetail[0].status}'
                                      : 'No'),
                              YachtInformation("Description",
                                  controller.yachtDetail[0].descriptions),
                            ],
                          ),
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
