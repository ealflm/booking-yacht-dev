//import 'dart:ui';
import 'package:flutter/material.dart';
import '/widgets/dropdown.dart';
import '../widgets/photo_album.dart';

List<String> imgArray = [
  "https://focusasiatravel.vn/wp-content/uploads/2019/05/sun-world-hon-thom-nature-park.jpg?fit=crop&w=240&q=80",
  "https://rootytrip.com/wp-content/uploads/2020/02/tour-phu-quoc-8-diem-tren-can.jpg?fit=crop&w=240&q=80",
  "https://vietyouth.vn/wp-content/uploads/2018/10/28-dia-diem-du-lich-phu-quoc-moi-ve-dem-mien-phi-tong-hop-tu-a-z-12552-5.jpg?fit=crop&w=240&q=80",
  "https://vietyouth.vn/wp-content/uploads/2018/10/28-dia-diem-du-lich-phu-quoc-moi-ve-dem-mien-phi-tong-hop-tu-a-z-12552-5.jpg?fit=crop&w=240&q=80",
  "https://focusasiatravel.vn/wp-content/uploads/2019/05/sun-world-hon-thom-nature-park.jpg?fit=crop&w=240&q=80",
  "https://rootytrip.com/wp-content/uploads/2020/02/tour-phu-quoc-8-diem-tren-can.jpg?fit=crop&w=240&q=80"
];

class TourDetail extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        extendBodyBehindAppBar: true,
        appBar: AppBar(
          backgroundColor: Color(0x0000000000),
          elevation: 0,
        ),
        body: Stack(
          children: [
            Container(
              height: MediaQuery.of(context).size.height * 0.6,
              decoration: BoxDecoration(
                  image: DecorationImage(
                      alignment: Alignment.topCenter,
                      image: NetworkImage(
                          "https://en.dangcongsan.vn/DATA/3/2020/04/phu_quoc-16_49_12_204.jpg"),
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
            Container(
              margin: EdgeInsets.only(
                top: MediaQuery.of(context).size.height * 0.10,
              ),
              padding: EdgeInsets.symmetric(horizontal: 28),
              child: Column(
                mainAxisAlignment: MainAxisAlignment.start,
                crossAxisAlignment: CrossAxisAlignment.start,
                children: [
                  Padding(
                    padding: const EdgeInsets.only(bottom: 4.0),
                    child: Text("Du lịch Phú Quốc",
                        style: TextStyle(fontSize: 28, color: Colors.white)),
                  ),
                  Row(
                    mainAxisAlignment: MainAxisAlignment.spaceBetween,
                    children: [
                      Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          Row(
                            children: [
                              Padding(
                                padding: const EdgeInsets.only(right: 4.0),
                                child: Text("4.8",
                                    style: TextStyle(
                                        color: Colors.orange[600],
                                        fontSize: 16)),
                              ),
                              Icon(Icons.star_border,
                                  color: Colors.orange, size: 20)
                            ],
                          )
                        ],
                      ),
                    ],
                  )
                ],
              ),
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
                        borderRadius: BorderRadius.only(
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
                                  Text("\$33",
                                      style: TextStyle(
                                          fontWeight: FontWeight.w600,
                                          fontSize: 16)),
                                  SizedBox(
                                    height: 6,
                                  ),
                                  Text(
                                    "Price",
                                  )
                                  // style:
                                  // TextStyle(color: MaterialColors.muted)
                                  // )
                                ],
                              ),
                              Column(
                                children: [
                                  Text("5",
                                      style: TextStyle(
                                          fontWeight: FontWeight.w600,
                                          fontSize: 16)),
                                  SizedBox(
                                    height: 6,
                                  ),
                                  Text(
                                    "Person",
                                  )
                                  // style:
                                  //     TextStyle(color: MaterialColors.muted))
                                ],
                              ),
                              Column(
                                children: [
                                  Text("2",
                                      style: TextStyle(
                                          fontWeight: FontWeight.w600,
                                          fontSize: 16)),
                                  SizedBox(
                                    height: 6,
                                  ),
                                  Text("Hours",
                                      style: TextStyle(color: Colors.black))
                                ],
                              ),
                            ],
                          ),
                          PhotoAlbum(imgArray),
                          Row(
                            children: [
                              Text('Choose your yacht: '),
                              Dropdown(),
                            ],
                          ),
                        ],
                      ),
                    )),
              ),
            )
          ],
        ));
  }
}
