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
    this.tittle,
    this.descriptions,
    this.status,
  });

  String? id;
  String? tittle;
  String? descriptions;
  int? status;

  factory Tour.fromJson(Map<String, dynamic> json) => Tour(
        id: json["id"],
        tittle: json["tittle"],
        descriptions: json["descriptions"],
        status: json["status"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "tittle": tittle,
        "descriptions": descriptions,
        "status": status,
      };
}
