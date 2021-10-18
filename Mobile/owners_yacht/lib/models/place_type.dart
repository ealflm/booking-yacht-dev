// To parse this JSON data, do
//
//     final placeTypeReponse = placeTypeReponseFromJson(jsonString);

import 'dart:convert';

PlaceTypeReponse placeTypeReponseFromJson(String str) =>
    PlaceTypeReponse.fromJson(json.decode(str));

String placeTypeReponseToJson(PlaceTypeReponse data) =>
    json.encode(data.toJson());

class PlaceTypeReponse {
  PlaceTypeReponse({
    this.data,
  });

  List<PlaceType>? data;

  factory PlaceTypeReponse.fromJson(Map<String, dynamic> json) =>
      PlaceTypeReponse(
        data: List<PlaceType>.from(
            json["data"].map((x) => PlaceType.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data!.map((x) => x.toJson())),
      };
}

class PlaceType {
  PlaceType({
    this.id,
    this.name,
    this.status,
  });

  String? id;
  String? name;
  int? status;

  factory PlaceType.fromJson(Map<String, dynamic> json) => PlaceType(
        id: json["id"],
        name: json["name"],
        status: json["status"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "name": name,
        "status": status,
      };
}
