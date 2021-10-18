// To parse this JSON data, do
//
//     final destinationsTourReponse = destinationsTourReponseFromJson(jsonString);

import 'dart:convert';

DestinationsTourReponse destinationTourReponseFromJson(String str) =>
    DestinationsTourReponse.fromJson(json.decode(str));

String destinationTourReponseToJson(DestinationsTourReponse data) =>
    json.encode(data.toJson());

class DestinationsTourReponse {
  DestinationsTourReponse({
    this.data,
  });

  List<DestinationTour>? data;

  factory DestinationsTourReponse.fromJson(Map<String, dynamic> json) =>
      DestinationsTourReponse(
        data: List<DestinationTour>.from(
            json["data"].map((x) => DestinationTour.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data!.map((x) => x.toJson())),
      };
}

class DestinationTour {
  DestinationTour({
    this.id,
    this.idDestination,
    this.idTour,
    this.status,
    this.way,
  });

  String? id;
  String? idDestination;
  String? idTour;
  int? status;
  int? way;

  factory DestinationTour.fromJson(Map<String, dynamic> json) =>
      DestinationTour(
        id: json["id"],
        idDestination: json["idDestination"],
        idTour: json["idTour"],
        status: json["status"],
        way: json["way"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "idDestination": idDestination,
        "idTour": idTour,
        "status": status,
        "way": way,
      };
}
