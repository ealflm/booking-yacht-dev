// To parse this JSON data, do
//
//     final businessTourReponse = businessTourReponseFromJson(jsonString);

import 'dart:convert';

import 'package:owners_yacht/models/ticket_type.dart';

BusinessTourReponse businessTourReponseFromJson(String str) =>
    BusinessTourReponse.fromJson(json.decode(str));

String businessTourReponseToJson(BusinessTourReponse data) =>
    json.encode(data.toJson());

class BusinessTourReponse {
  BusinessTourReponse({
    this.data,
  });

  List<BusinessTour>? data;

  factory BusinessTourReponse.fromJson(Map<String, dynamic> json) =>
      BusinessTourReponse(
        data: List<BusinessTour>.from(
            json["data"].map((x) => BusinessTour.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data!.map((x) => x.toJson())),
      };
}

class BusinessTour {
  BusinessTour({
    this.id,
    this.idBusiness,
    this.idTour,
    this.status,
    this.idBusinessNavigation,
    this.idTourNavigation,
    this.ticketTypes,
    this.trips,
  });

  String? id;
  String? idBusiness;
  String? idTour;
  int? status;
  IdBusinessNavigation? idBusinessNavigation;
  IdTourNavigation? idTourNavigation;
  List<TicketType>? ticketTypes;
  List<TripNavigation>? trips;

  factory BusinessTour.fromJson(Map<String, dynamic> json) => BusinessTour(
        id: json["id"],
        idBusiness: json["idBusiness"],
        idTour: json["idTour"],
        status: json["status"],
        idBusinessNavigation:
            IdBusinessNavigation.fromJson(json["idBusinessNavigation"]),
        idTourNavigation: IdTourNavigation.fromJson(json["idTourNavigation"]),
        ticketTypes: List<TicketType>.from(
            json["ticketTypes"].map((x) => TicketType.fromJson(x))),
        trips: List<TripNavigation>.from(json["trips"].map((x) => TripNavigation.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "idBusiness": idBusiness,
        "idTour": idTour,
        "status": status,
        "idBusinessNavigation": idBusinessNavigation!.toJson(),
        "idTourNavigation": idTourNavigation!.toJson(),
        "ticketTypes": List<dynamic>.from(ticketTypes!.map((x) => x.toJson())),
        "trips": List<dynamic>.from(trips!.map((x) => x.toJson())),
      };
}

class IdBusinessNavigation {
  IdBusinessNavigation({
    this.id,
    this.uid,
    this.name,
    this.emailAddress,
    this.password,
    this.salt,
    this.phoneNumber,
    this.photoUrl,
    this.address,
    this.status,
    this.vnpTmnCode,
    this.vnpHashSecret,
  });

  String? id;
  dynamic uid;
  String? name;
  String? emailAddress;
  dynamic password;
  dynamic salt;
  String? phoneNumber;
  dynamic photoUrl;
  String? address;
  int? status;
  String? vnpTmnCode;
  String? vnpHashSecret;

  factory IdBusinessNavigation.fromJson(Map<String, dynamic> json) =>
      IdBusinessNavigation(
        id: json["id"],
        uid: json["uid"],
        name: json["name"],
        emailAddress: json["emailAddress"],
        password: json["password"],
        salt: json["salt"],
        phoneNumber: json["phoneNumber"],
        photoUrl: json["photoUrl"],
        address: json["address"],
        status: json["status"],
        vnpTmnCode: json["vnpTmnCode"],
        vnpHashSecret: json["vnpHashSecret"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "uid": uid,
        "name": name,
        "emailAddress": emailAddress,
        "password": password,
        "salt": salt,
        "phoneNumber": phoneNumber,
        "photoUrl": photoUrl,
        "address": address,
        "status": status,
        "vnpTmnCode": vnpTmnCode,
        "vnpHashSecret": vnpHashSecret,
      };
}

class IdTourNavigation {
  IdTourNavigation({
    this.id,
    this.title,
    this.descriptions,
    this.status,
    this.imageLink,
  });

  String? id;
  String? title;
  String? descriptions;
  int? status;
  String? imageLink;

  factory IdTourNavigation.fromJson(Map<String, dynamic> json) =>
      IdTourNavigation(
        id: json["id"],
        title: json["title"],
        descriptions: json["descriptions"],
        status: json["status"],
        imageLink: json["imageLink"] == null ? null : json["imageLink"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "title": title,
        "descriptions": descriptions,
        "status": status,
        "imageLink": imageLink == null ? null : imageLink,
      };
}

class TripNavigation {
  TripNavigation({
    this.id,
    this.time,
    this.idBusinessTour,
    this.idVehicle,
    this.status,
    this.idTour,
    this.idBusiness,
    this.amountTicket,
    this.orders,
    this.idVehicleNavigation,
  });

  String? id;
  DateTime? time;
  String? idBusinessTour;
  String? idVehicle;
  int? status;
  String? idTour;
  String? idBusiness;
  int? amountTicket;
  List<Order>? orders;
  IdVehicleNavigation? idVehicleNavigation;

  factory TripNavigation.fromJson(Map<String, dynamic> json) => TripNavigation(
        id: json["id"],
        time: DateTime.parse(json["time"]),
        idBusinessTour: json["idBusinessTour"],
        idVehicle: json["idVehicle"],
        status: json["status"],
        idTour: json["idTour"],
        idBusiness: json["idBusiness"],
        amountTicket: json["amountTicket"],
        orders: List<Order>.from(json["orders"].map((x) => Order.fromJson(x))),
        idVehicleNavigation:
            IdVehicleNavigation.fromJson(json["idVehicleNavigation"]),
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "time": time!.toIso8601String(),
        "idBusinessTour": idBusinessTour,
        "idVehicle": idVehicle,
        "status": status,
        "idTour": idTour,
        "idBusiness": idBusiness,
        "amountTicket": amountTicket,
        "orders": List<dynamic>.from(orders!.map((x) => x.toJson())),
        "idVehicleNavigation": idVehicleNavigation!.toJson(),
      };
}

class IdVehicleNavigation {
  IdVehicleNavigation({
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
    this.idBusinessNavigation,
    this.idVehicleTypeNavigation,
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
  dynamic imageLink;
  dynamic idBusinessNavigation;
  dynamic idVehicleTypeNavigation;

  factory IdVehicleNavigation.fromJson(Map<String, dynamic> json) =>
      IdVehicleNavigation(
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
        idBusinessNavigation: json["idBusinessNavigation"],
        idVehicleTypeNavigation: json["idVehicleTypeNavigation"],
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
        "idBusinessNavigation": idBusinessNavigation,
        "idVehicleTypeNavigation": idVehicleTypeNavigation,
      };
}

class Order {
  Order({
    this.id,
    this.agencyName,
    this.quantityOfPerson,
    this.totalPrice,
    this.idAgency,
    this.status,
    this.dateOrder,
    this.idTrip,
    this.idAgencyNavigation,
    this.idTripNavigation,
  });

  String? id;
  String? agencyName;
  int? quantityOfPerson;
  double? totalPrice;
  String? idAgency;
  int? status;
  DateTime? dateOrder;
  String? idTrip;
  dynamic idAgencyNavigation;
  dynamic idTripNavigation;

  factory Order.fromJson(Map<String, dynamic> json) => Order(
        id: json["id"],
        agencyName: json["agencyName"],
        quantityOfPerson: json["quantityOfPerson"],
        totalPrice: json["totalPrice"],
        idAgency: json["idAgency"],
        status: json["status"],
        dateOrder: DateTime.parse(json["dateOrder"]),
        idTrip: json["idTrip"],
        idAgencyNavigation: json["idAgencyNavigation"],
        idTripNavigation: json["idTripNavigation"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "agencyName": agencyName,
        "quantityOfPerson": quantityOfPerson,
        "totalPrice": totalPrice,
        "idAgency": idAgency,
        "status": status,
        "dateOrder": dateOrder!.toIso8601String(),
        "idTrip": idTrip,
        "idAgencyNavigation": idAgencyNavigation,
        "idTripNavigation": idTripNavigation,
      };
}
