import 'dart:convert';

// Yacht yachtFromJson(String str) => Yacht.fromJson(json.decode(str));

// String yachtToJson(Yacht data) => json.encode(data.toJson());

// class Yacht {
//   String id;
//   String name;
//   int seat;
//   String descriptions;
//   int status;
//   Yacht({
//     required this.id,
//     required this.name,
//     required this.seat,
//     required this.descriptions,
//     required this.status,
//   });

//   factory Yacht.fromJson(Map<String, dynamic> json) => Yacht(
//         id: json['id'],
//         name: json['name'],
//         seat: json['seat'] as int,
//         descriptions: json['descriptions'],
//         status: json['status'] as int,
//       );

//   Map<String, dynamic> toJson() => {
//         "id": id,
//         "name": name,
//         "seat": seat,
//         "descriptions": descriptions,
//         "status": status,
//       };
// }

// To parse this JSON data, do
//
//     final yacht = yachtFromJson(jsonString);

import 'dart:convert';

YachtReponse yachtFromJson(String str) =>
    YachtReponse.fromJson(json.decode(str));

String yachtToJson(YachtReponse data) => json.encode(data.toJson());

class YachtReponse {
  YachtReponse({
    required this.data,
  });

  List<Yacht> data;

  factory YachtReponse.fromJson(Map<String, dynamic> json) => YachtReponse(
        data: List<Yacht>.from(json["data"].map((x) => Yacht.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data.map((x) => x.toJson())),
      };
}

class Yacht {
  Yacht({
    required this.id,
    required this.name,
    required this.seat,
    required this.descriptions,
    required this.idVehicleType,
    required this.idBusiness,
    required this.status,
  });

  String id;
  String name;
  int seat;
  String descriptions;
  String idVehicleType;
  String idBusiness;
  int status;

  factory Yacht.fromJson(Map<String, dynamic> json) => Yacht(
        id: json["id"],
        name: json["name"],
        seat: json["seat"],
        descriptions: json["descriptions"],
        idVehicleType: json["idVehicleType"],
        idBusiness: json["idBusiness"],
        status: json["status"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "name": name,
        "seat": seat,
        "descriptions": descriptions,
        "idVehicleType": idVehicleType,
        "idBusiness": idBusiness,
        "status": status,
      };
}
