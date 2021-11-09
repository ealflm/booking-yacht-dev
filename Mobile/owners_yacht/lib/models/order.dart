// To parse this JSON data, do
//
//     final orderReponse = orderReponseFromJson(jsonString);

import 'dart:convert';

OrderReponse orderReponseFromJson(String str) => OrderReponse.fromJson(json.decode(str));

String orderReponseToJson(OrderReponse data) => json.encode(data.toJson());

class OrderReponse {
    OrderReponse({
        this.data,
    });

    List<Order>? data;

    factory OrderReponse.fromJson(Map<String, dynamic> json) => OrderReponse(
        data: List<Order>.from(json["data"].map((x) => Order.fromJson(x))),
    );

    Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data!.map((x) => x.toJson())),
    };
}

class Order {
    Order({
        this.id,
        this.tourName,
        this.agencyName,
        this.quantityOfPerson,
        this.totalPrice,
        this.idAgency,
        this.status,
        this.idTrip,
        this.orderDate,
        this.agencyViewModels,
    });

    String? id;
    String? tourName;
    String? agencyName;
    int? quantityOfPerson;
    double? totalPrice;
    String? idAgency;
    int? status;
    String? idTrip;
    DateTime? orderDate;
    AgencyViewModels? agencyViewModels;

    factory Order.fromJson(Map<String, dynamic> json) => Order(
        id: json["id"],
        tourName: json["tourName"],
        agencyName: json["agencyName"],
        quantityOfPerson: json["quantityOfPerson"],
        totalPrice: json["totalPrice"],
        idAgency: json["idAgency"],
        status: json["status"],
        idTrip: json["idTrip"],
        orderDate: DateTime.parse(json["orderDate"]),
        agencyViewModels: AgencyViewModels.fromJson(json["agencyViewModels"]),
    );

    Map<String, dynamic> toJson() => {
        "id": id,
        "tourName": tourName,
        "agencyName": agencyName,
        "quantityOfPerson": quantityOfPerson,
        "totalPrice": totalPrice,
        "idAgency": idAgency,
        "status": status,
        "idTrip": idTrip,
        "orderDate": orderDate!.toIso8601String(),
        "agencyViewModels": agencyViewModels!.toJson(),
    };
}

class AgencyViewModels {
    AgencyViewModels({
        this.id,
        this.name,
        this.address,
        this.emailAddress,
        this.phoneNumber,
        this.status,
        this.photoUrl,
    });

    String? id;
    String? name;
    dynamic address;
    String? emailAddress;
    String? phoneNumber;
    int? status;
    String? photoUrl;
    factory AgencyViewModels.fromJson(Map<String, dynamic> json) => AgencyViewModels(
        id: json["id"],
        name: json["name"],
        address: json["address"],
        emailAddress: json["emailAddress"],
        phoneNumber: json["phoneNumber"],
        status: json["status"],
        photoUrl: json["photoUrl"],
    );

    Map<String, dynamic> toJson() => {
        "id": id,
        "name": name,
        "address": address,
        "emailAddress": emailAddress,
        "phoneNumber": phoneNumber,
        "status": status,
        "photoUrl": photoUrl,
    };
}
