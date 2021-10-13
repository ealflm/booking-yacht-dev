import 'dart:convert';

CategoryReponse categoryReponseFromJson(String str) =>
    CategoryReponse.fromJson(json.decode(str));

String categoryReponseToJson(CategoryReponse data) =>
    json.encode(data.toJson());

class CategoryReponse {
  CategoryReponse({
    required this.data,
  });

  List<Category> data;

  factory CategoryReponse.fromJson(Map<String, dynamic> json) =>
      CategoryReponse(
        data:
            List<Category>.from(json["data"].map((x) => Category.fromJson(x))),
      );

  Map<String, dynamic> toJson() => {
        "data": List<dynamic>.from(data.map((x) => x.toJson())),
      };
}

class Category {
  Category({
    required this.id,
    required this.name,
    required this.status,
  });

  String id;
  String name;
  int status;

  factory Category.fromJson(Map<String, dynamic> json) => Category(
        id: json["id"],
        name: json["name"],
        status: json["status"],
      );

  Map<String, dynamic> toJson() => {
        "id": id,
        "name": name,
        "status": status,
      };
}
