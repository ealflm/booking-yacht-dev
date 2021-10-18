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
    this.nameCustomer,
    this.phone,
    this.idOrder,
    this.idTicketType,
    this.idTrip,
    this.price,
    this.status,
    this.idOrderNavigation,
    this.idTicketTypeNavigation,
    this.idTripNavigation,
  });

  String? id;
  String? nameCustomer;
  String? phone;
  String? idOrder;
  String? idTicketType;
  String? idTrip;
  double? price;
  int? status;
  IdOrderNavigation? idOrderNavigation;
  IdTicketTypeNavigation? idTicketTypeNavigation;
  IdTripNavigation? idTripNavigation;

  factory Ticket.fromJson(Map<String, dynamic> json) => Ticket(
        id: json["id"],
        nameCustomer: json["nameCustomer"],
        phone: json["phone"],
        idOrder: json["idOrder"],
        idTicketType: json["idTicketType"],
        idTrip: json["idTrip"],
        price: json["price"].toDouble(),
        status: json["status"],
        idOrderNavigation:
            IdOrderNavigation.fromJson(json["idOrderNavigation"]),
        idTicketTypeNavigation:
            IdTicketTypeNavigation.fromJson(json["idTicketTypeNavigation"]),
        idTripNavigation: IdTripNavigation.fromJson(json["idTripNavigation"]),
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "nameCustomer": nameCustomer,
        "phone": phone,
        "idOrder": idOrder,
        "idTicketType": idTicketType,
        "idTrip": idTrip,
        "price": price,
        "status": status,
        "idOrderNavigation": idOrderNavigation!.toJson(),
        "idTicketTypeNavigation": idTicketTypeNavigation!.toJson(),
        "idTripNavigation": idTripNavigation!.toJson(),
      };
}

class IdOrderNavigation {
  IdOrderNavigation({
    this.id,
    this.agencyName,
    this.quantityOfPerson,
    this.totalPrice,
    this.idAgency,
    this.status,
    this.dateOrder,
    this.idAgencyNavigation,
  });

  String? id;
  String? agencyName;
  int? quantityOfPerson;
  double? totalPrice;
  String? idAgency;
  int? status;
  DateTime? dateOrder;
  dynamic idAgencyNavigation;

  factory IdOrderNavigation.fromJson(Map<String, dynamic> json) =>
      IdOrderNavigation(
        id: json["id"],
        agencyName: json["agencyName"],
        quantityOfPerson: json["quantityOfPerson"],
        totalPrice: json["totalPrice"].toDouble(),
        idAgency: json["idAgency"],
        status: json["status"],
        dateOrder: DateTime.parse(json["dateOrder"]),
        idAgencyNavigation: json["idAgencyNavigation"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "agencyName": agencyName,
        "quantityOfPerson": quantityOfPerson,
        "totalPrice": totalPrice,
        "idAgency": idAgency,
        "status": status,
        "dateOrder": dateOrder!.toIso8601String(),
        "idAgencyNavigation": idAgencyNavigation,
      };
}

class IdTicketTypeNavigation {
  IdTicketTypeNavigation({
    this.id,
    this.name,
    this.price,
    this.status,
    this.commissionFeePercentage,
    this.serviceFeePercentage,
    this.idBusinessTour,
    this.idBusinessTourNavigation,
  });

  String? id;
  String? name;
  int? price;
  int? status;
  int? commissionFeePercentage;
  int? serviceFeePercentage;
  String? idBusinessTour;
  dynamic idBusinessTourNavigation;

  factory IdTicketTypeNavigation.fromJson(Map<String, dynamic> json) =>
      IdTicketTypeNavigation(
        id: json["id"],
        name: json["name"],
        price: json["price"],
        status: json["status"],
        commissionFeePercentage: json["commissionFeePercentage"],
        serviceFeePercentage: json["serviceFeePercentage"],
        idBusinessTour: json["idBusinessTour"],
        idBusinessTourNavigation: json["idBusinessTourNavigation"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "name": name,
        "price": price,
        "status": status,
        "commissionFeePercentage": commissionFeePercentage,
        "serviceFeePercentage": serviceFeePercentage,
        "idBusinessTour": idBusinessTour,
        "idBusinessTourNavigation": idBusinessTourNavigation,
      };
}

class IdTripNavigation {
  IdTripNavigation({
    this.id,
    this.time,
    this.idBusiness,
    this.idVehicle,
    this.status,
    this.idBusinessNavigation,
    this.idVehicleNavigation,
  });

  String? id;
  DateTime? time;
  String? idBusiness;
  String? idVehicle;
  int? status;
  dynamic idBusinessNavigation;
  dynamic idVehicleNavigation;

  factory IdTripNavigation.fromJson(Map<String, dynamic> json) =>
      IdTripNavigation(
        id: json["id"],
        time: DateTime.parse(json["time"]),
        idBusiness: json["idBusiness"],
        idVehicle: json["idVehicle"],
        status: json["status"],
        idBusinessNavigation: json["idBusinessNavigation"],
        idVehicleNavigation: json["idVehicleNavigation"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "time": time!.toIso8601String(),
        "idBusiness": idBusiness,
        "idVehicle": idVehicle,
        "status": status,
        "idBusinessNavigation": idBusinessNavigation,
        "idVehicleNavigation": idVehicleNavigation,
      };
}
