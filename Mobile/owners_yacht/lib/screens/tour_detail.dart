//import 'dart:ui';
import 'package:flutter/material.dart';
import 'package:owners_yacht/constants/status.dart';
import 'package:owners_yacht/constants/theme.dart';
import 'package:owners_yacht/controller/tour.dart';
import 'package:owners_yacht/widgets/nav_bar.dart';
import '../widgets/photo_album.dart';
import 'package:get/get.dart';
import 'package:flutter_html/flutter_html.dart';

List<String> imgArray = [
  "https://focusasiatravel.vn/wp-content/uploads/2019/05/sun-world-hon-thom-nature-park.jpg?fit=crop&w=240&q=80",
  "https://rootytrip.com/wp-content/uploads/2020/02/tour-phu-quoc-8-diem-tren-can.jpg?fit=crop&w=240&q=80",
  "https://vietyouth.vn/wp-content/uploads/2018/10/28-dia-diem-du-lich-phu-quoc-moi-ve-dem-mien-phi-tong-hop-tu-a-z-12552-5.jpg?fit=crop&w=240&q=80",
  "https://vietyouth.vn/wp-content/uploads/2018/10/28-dia-diem-du-lich-phu-quoc-moi-ve-dem-mien-phi-tong-hop-tu-a-z-12552-5.jpg?fit=crop&w=240&q=80",
  "https://focusasiatravel.vn/wp-content/uploads/2019/05/sun-world-hon-thom-nature-park.jpg?fit=crop&w=240&q=80",
  "https://rootytrip.com/wp-content/uploads/2020/02/tour-phu-quoc-8-diem-tren-can.jpg?fit=crop&w=240&q=80"
];

class TourDetail extends StatelessWidget {
  TourController _tourController = Get.find<TourController>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      extendBodyBehindAppBar: true,
      appBar: NavBar(
        title: _tourController.tourDetail.title!,
        color: BookingYachtColors.appBarDetail,
      ),
      body: Stack(
        children: [
          Container(
            height: MediaQuery.of(context).size.height * 0.6,
            decoration: BoxDecoration(
                image: DecorationImage(
                    alignment: Alignment.topCenter,
                    image: NetworkImage(
                        _tourController.tourDetail.imageLink ??
                                  'https://www.publicdomainpictures.net/pictures/280000/velka/not-found-image-15383864787lu.jpg'),
                    fit: BoxFit.cover)),
          ),
          Container(
            height: MediaQuery.of(context).size.height * 0.6,
            decoration: BoxDecoration(
                gradient: LinearGradient(
                    begin: Alignment.center,
                    end: Alignment.bottomCenter,
                    colors: [
                  Colors.black.withOpacity(0),
                  Colors.black.withOpacity(0.9),
                ])),
          ),
          SingleChildScrollView(
            child: Padding(
              padding: const EdgeInsets.symmetric(horizontal: 2.5),
              child: Container(
                decoration: BoxDecoration(
                    boxShadow: [
                      BoxShadow(
                          color: Colors.black.withOpacity(0.5),
                          spreadRadius: 8,
                          blurRadius: 10,
                          offset: Offset(0, 0))
                    ],
                    color: Colors.white,
                    borderRadius: const BorderRadius.only(
                      topLeft: Radius.circular(13.0),
                      topRight: Radius.circular(13.0),
                    )),
                margin: EdgeInsets.only(
                  top: MediaQuery.of(context).size.height * 0.58,
                ),
                alignment: Alignment.bottomCenter,
                child: Padding(
                  padding: const EdgeInsets.symmetric(
                      horizontal: 18.0, vertical: 12.0),
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.spaceAround,
                    children: [
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceAround,
                        children: [
                          Column(
                            children: [
                              Text(
                                  "Trạng thái: ${BookingYachtStatus.status[_tourController.tourDetail.status].toString()}",
                                  style: const TextStyle(
                                      fontWeight: FontWeight.w600,
                                      fontSize: 16)),
                              // SizedBox(
                              //   height: 6,
                              // ),
                              // Text(
                              //   "Price",
                              // )
                              // style:
                              // TextStyle(color: MaterialColors.muted)
                              // )
                            ],
                          ),
                          // Column(
                          //   children: [
                          //     Text("5",
                          //         style: TextStyle(
                          //             fontWeight: FontWeight.w600,
                          //             fontSize: 16)),
                          //     SizedBox(
                          //       height: 6,
                          //     ),
                          //     Text(
                          //       "Person",
                          //     )
                          //     // style:
                          //     //     TextStyle(color: MaterialColors.muted))
                          //   ],
                          // ),
                          // Column(
                          //   children: [
                          //     Text("2",
                          //         style: TextStyle(
                          //             fontWeight: FontWeight.w600,
                          //             fontSize: 16)),
                          //     SizedBox(
                          //       height: 6,
                          //     ),
                          //     Text("Hours",
                          //         style: TextStyle(color: Colors.black))
                          //   ],
                          // ),
                        ],
                      ),
                      PhotoAlbum(imgArray),
                      Center(
                        child: Html(
                          data: """
                ${_tourController.tourDetail.descriptions}
                """,
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            ),
          )
        ],
      ),
    );
  }
}
