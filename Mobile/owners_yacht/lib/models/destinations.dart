// To parse this JSON data, do
//
//     final destinationsReponse = destinationsReponseFromJson(jsonString);

import 'dart:convert';

DestinationsReponse destinationsReponseFromJson(String str) =>
    DestinationsReponse.fromJson(json.decode(str));

String destinationsReponseToJson(DestinationsReponse data) =>
    json.encode(data.toJson());

class DestinationsReponse {
  DestinationsReponse({
    this.data,
  });

  List<Destinations>? data;

  factory DestinationsReponse.fromJson(Map<String, dynamic> json) =>
      DestinationsReponse(
        data: List<Destinations>.from(
            json["data"].map((x) => Destinations.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data!.map((x) => x.toJson())),
      };
}

class Destinations {
  Destinations({
    this.id,
    this.name,
    this.address,
    this.status,
    this.idPlaceType,
  });

  String? id;
  String? name;
  String? address;
  int? status;
  String? idPlaceType;

  factory Destinations.fromJson(Map<String, dynamic> json) => Destinations(
        id: json["id"],
        name: json["name"],
        address: json["address"],
        status: json["status"],
        idPlaceType: json["idPlaceType"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "name": name,
        "address": address,
        "status": status,
        "idPlaceType": idPlaceType,
      };
}
