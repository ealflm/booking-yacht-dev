// To parse this JSON data, do
//
//     final ticketTypeReponse = ticketTypeReponseFromJson(jsonString);

import 'dart:convert';

TicketTypeReponse ticketTypeReponseFromJson(String str) =>
    TicketTypeReponse.fromJson(json.decode(str));

String ticketTypeReponseToJson(TicketTypeReponse data) =>
    json.encode(data.toJson());

class TicketTypeReponse {
  TicketTypeReponse({
    this.data,
  });

  List<TicketType>? data;

  factory TicketTypeReponse.fromJson(Map<String, dynamic> json) =>
      TicketTypeReponse(
        data: List<TicketType>.from(
            json["data"].map((x) => TicketType.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data!.map((x) => x.toJson())),
      };
}

class TicketType {
  TicketType({
    this.id,
    this.name,
    this.price,
    this.status,
    this.commissionFeePercentage,
    this.serviceFeePercentage,
    this.idBusinessTour,
    this.idTour,
    this.idBusiness,
  });

  String? id;
  String? name;
  double? price;
  int? status;
  double? commissionFeePercentage;
  double? serviceFeePercentage;
  String? idBusinessTour;
  String? idTour;
  String? idBusiness;

  factory TicketType.fromJson(Map<String, dynamic> json) => TicketType(
        id: json["id"],
        name: json["name"],
        price: json["price"],
        status: json["status"],
        commissionFeePercentage: json["commissionFeePercentage"],
        serviceFeePercentage: json["serviceFeePercentage"],
        idBusinessTour: json["idBusinessTour"],
        idTour: json["idTour"],
        idBusiness: json["idBusiness"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "name": name,
        "price": price,
        "status": status,
        "commissionFeePercentage": commissionFeePercentage,
        "serviceFeePercentage": serviceFeePercentage,
        "idBusinessTour": idBusinessTour,
        "idTour": idTour,
        "idBusiness": idBusiness,
      };
}
