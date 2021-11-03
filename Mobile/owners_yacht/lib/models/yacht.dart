// To parse this JSON data, do
//
//     final yachtReponse = yachtReponseFromJson(jsonString);

import 'dart:convert';

YachtReponse yachtReponseFromJson(String str) => YachtReponse.fromJson(json.decode(str));

String yachtReponseToJson(YachtReponse data) => json.encode(data.toJson());

class YachtReponse {
    YachtReponse({
        this.data,
    });

    List<Yacht>? data;

    factory YachtReponse.fromJson(Map<String, dynamic> json) => YachtReponse(
        data: List<Yacht>.from(json["data"].map((x) => Yacht.fromJson(x))),
    );

    Map<String, dynamic> toJson() => {
        "data": List<Yacht>.from(data!.map((x) => x.toJson())),
    };
}

class Yacht {
    Yacht({
        this.id,
        this.name,
        this.registrationNumber,
        this.yearOfManufacture,
        this.whereProduction,
        this.seat,
        this.descriptions,
        this.idVehicleType,
        this.idBusiness,
        this.status,
        this.imageLink,
    });

    String? id;
    String? name;
    String? registrationNumber;
    int? yearOfManufacture;
    String? whereProduction;
    int? seat;
    String? descriptions;
    String? idVehicleType;
    String? idBusiness;
    int? status;
    String? imageLink;

    factory Yacht.fromJson(Map<String, dynamic> json) => Yacht(
        id: json["id"],
        name: json["name"],
        registrationNumber: json["registrationNumber"],
        yearOfManufacture: json["yearOfManufacture"],
        whereProduction: json["whereProduction"],
        seat: json["seat"],
        descriptions: json["descriptions"],
        idVehicleType: json["idVehicleType"],
        idBusiness: json["idBusiness"],
        status: json["status"],
        imageLink: json["imageLink"],
    );

    Map<String, dynamic> toJson() => {
        "id": id,
        "name": name,
        "registrationNumber": registrationNumber,
        "yearOfManufacture": yearOfManufacture,
        "whereProduction": whereProduction,
        "seat": seat,
        "descriptions": descriptions,
        "idVehicleType": idVehicleType,
        "idBusiness": idBusiness,
        "status": status,
        "imageLink": imageLink,
    };
}
