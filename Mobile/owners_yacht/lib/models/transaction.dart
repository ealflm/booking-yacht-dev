// To parse this JSON data, do
//
//     final transactionReponse = transactionReponseFromJson(jsonString);

import 'dart:convert';

TransactionReponse transactionReponseFromJson(String str) =>
    TransactionReponse.fromJson(json.decode(str));

String transactionReponseToJson(TransactionReponse data) =>
    json.encode(data.toJson());

class TransactionReponse {
  TransactionReponse({
    this.data,
  });

  List<Transaction>? data;

  factory TransactionReponse.fromJson(Map<String, dynamic> json) =>
      TransactionReponse(
        data: List<Transaction>.from(
            json["data"].map((x) => Transaction.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data!.map((x) => x.toJson())),
      };
}

class Transaction {
  Transaction({
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
  IdAgencyNavigation? idAgencyNavigation;

  factory Transaction.fromJson(Map<String, dynamic> json) => Transaction(
        id: json["id"],
        agencyName: json["agencyName"],
        quantityOfPerson: json["quantityOfPerson"],
        totalPrice: json["totalPrice"].toDouble(),
        idAgency: json["idAgency"],
        status: json["status"],
        dateOrder: json["dateOrder"] == null
            ? null
            : DateTime.parse(json["dateOrder"]),
        idAgencyNavigation:
            IdAgencyNavigation.fromJson(json["idAgencyNavigation"]),
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "agencyName": agencyName,
        "quantityOfPerson": quantityOfPerson,
        "totalPrice": totalPrice,
        "idAgency": idAgency,
        "status": status,
        "dateOrder": dateOrder == null ? null : dateOrder!.toIso8601String(),
        "idAgencyNavigation": idAgencyNavigation!.toJson(),
      };
}

class IdAgencyNavigation {
  IdAgencyNavigation({
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

  factory IdAgencyNavigation.fromJson(Map<String, dynamic> json) =>
      IdAgencyNavigation(
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
      };
}
