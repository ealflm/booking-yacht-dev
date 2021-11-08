// To parse this JSON data, do
//
//     final ticketReponse = ticketReponseFromJson(jsonString);

import 'dart:convert';

TicketReponse ticketReponseFromJson(String str) => TicketReponse.fromJson(json.decode(str));

String ticketReponseToJson(TicketReponse data) => json.encode(data.toJson());

class TicketReponse {
    TicketReponse({
        this.data,
    });

    Ticket? data;

    factory TicketReponse.fromJson(Map<String, dynamic> json) => TicketReponse(
        data: Ticket.fromJson(json["data"]),
    );

    Map<String, dynamic> toJson() => {
        "data": data!.toJson(),
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
    dynamic idOrderNavigation;
    dynamic idTicketTypeNavigation;
    dynamic idTripNavigation;

    factory Ticket.fromJson(Map<String, dynamic> json) => Ticket(
        id: json["id"],
        nameCustomer: json["nameCustomer"],
        phone: json["phone"],
        idOrder: json["idOrder"],
        idTicketType: json["idTicketType"],
        idTrip: json["idTrip"],
        price: json["price"],
        status: json["status"],
        idOrderNavigation: json["idOrderNavigation"],
        idTicketTypeNavigation: json["idTicketTypeNavigation"],
        idTripNavigation: json["idTripNavigation"],
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
        "idOrderNavigation": idOrderNavigation,
        "idTicketTypeNavigation": idTicketTypeNavigation,
        "idTripNavigation": idTripNavigation,
    };
}
