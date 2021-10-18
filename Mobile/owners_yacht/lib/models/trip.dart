// To parse this JSON data, do
//
//     final tripReponse = tripReponseFromJson(jsonString);

import 'dart:convert';

TripReponse tripReponseFromJson(String str) =>
    TripReponse.fromJson(json.decode(str));

String tripReponseToJson(TripReponse data) => json.encode(data.toJson());

class TripReponse {
  TripReponse({
    this.data,
  });

  List<Trip>? data;

  factory TripReponse.fromJson(Map<String, dynamic> json) => TripReponse(
        data: List<Trip>.from(json["data"].map((x) => Trip.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data!.map((x) => x.toJson())),
      };
}

class Trip {
  Trip({
    this.id,
    this.time,
    this.idBusiness,
    this.idVehicle,
    this.status,
  });

  String? id;
  DateTime? time;
  String? idBusiness;
  String? idVehicle;
  int? status;

  factory Trip.fromJson(Map<String, dynamic> json) => Trip(
        id: json["id"],
        time: DateTime.parse(json["time"]),
        idBusiness: json["idBusiness"],
        idVehicle: json["idVehicle"],
        status: json["status"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "time": time!.toIso8601String(),
        "idBusiness": idBusiness,
        "idVehicle": idVehicle,
        "status": status,
      };
}
