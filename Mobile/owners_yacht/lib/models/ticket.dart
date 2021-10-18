// To parse this JSON data, do
//
//     final ticketReponse = ticketReponseFromJson(jsonString);

import 'dart:convert';

TicketReponse ticketReponseFromJson(String str) =>
    TicketReponse.fromJson(json.decode(str));

String ticketReponseToJson(TicketReponse data) => json.encode(data.toJson());

class TicketReponse {
  TicketReponse({
    this.data,
  });

  List<Ticket>? data;

  factory TicketReponse.fromJson(Map<String, dynamic> json) => TicketReponse(
        data: List<Ticket>.from(json["data"].map((x) => Ticket.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data!.map((x) => x.toJson())),
      };
}

class Ticket {
  Ticket({
    this.id,
    this.name,
    this.price,
    this.status,
    this.commissionFeePercentage,
    this.serviceFeePercentage,
    this.idBusinessTour,
  });

  String? id;
  String? name;
  int? price;
  int? status;
  int? commissionFeePercentage;
  int? serviceFeePercentage;
  String? idBusinessTour;

  factory Ticket.fromJson(Map<String, dynamic> json) => Ticket(
        id: json["id"],
        name: json["name"],
        price: json["price"],
        status: json["status"],
        commissionFeePercentage: json["commissionFeePercentage"],
        serviceFeePercentage: json["serviceFeePercentage"],
        idBusinessTour: json["idBusinessTour"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "name": name,
        "price": price,
        "status": status,
        "commissionFeePercentage": commissionFeePercentage,
        "serviceFeePercentage": serviceFeePercentage,
        "idBusinessTour": idBusinessTour,
      };
}
