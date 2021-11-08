// To parse this JSON data, do
//
//     final tourReponse = tourReponseFromJson(jsonString);

import 'dart:convert';

TourReponse tourReponseFromJson(String str) =>
    TourReponse.fromJson(json.decode(str));

String tourReponseToJson(TourReponse data) => json.encode(data.toJson());

class TourReponse {
  TourReponse({
    this.data,
  });

  List<Tour>? data;

  factory TourReponse.fromJson(Map<String, dynamic> json) => TourReponse(
        data: List<Tour>.from(json["data"].map((x) => Tour.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data!.map((x) => x.toJson())),
      };
}

class Tour {
  Tour({
    this.id,
    this.title,
    this.descriptions,
    this.status,
    this.imageLink
  });

  String? id;
  String? title;
  String? descriptions;
  int? status;
  String? imageLink;

  factory Tour.fromJson(Map<String, dynamic> json) => Tour(
        id: json["id"],
        title: json["title"],
        descriptions: json["descriptions"],
        status: json["status"],
        imageLink: json["imageLink"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "title": title,
        "descriptions": descriptions,
        "status": status,
        "imageLink": imageLink,
      };
}
