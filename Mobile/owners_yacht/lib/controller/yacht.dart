import 'dart:io';

import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/models/category.dart';
import 'package:owners_yacht/screens/yacht_detail.dart';
import 'package:owners_yacht/screens/yacht_modify.dart';
import 'package:owners_yacht/screens/yachts.dart';
import 'package:shared_preferences/shared_preferences.dart';
import '/models/yacht.dart';
import 'package:http/http.dart' as http;
import 'dart:convert';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:image_picker/image_picker.dart';

class YachtController extends GetxController {
  final GlobalKey<FormState> yachtFormKey = GlobalKey<FormState>();
  var isLoading = true.obs;

  List<Yacht> listYacht = <Yacht>[].obs;
  List<Category> listCategory = <Category>[].obs;
  var yachtDetail = Yacht();

  bool isAdding = true;
  final ImagePicker _picker = ImagePicker();

  String id = "";
  File? image;
  String? imageLink = "";
  var categoryController = "";

  TextEditingController nameController = TextEditingController();
  TextEditingController seatController = TextEditingController();
  TextEditingController registrationNumberController = TextEditingController();
  TextEditingController yearOfManufactureController = TextEditingController();
  TextEditingController whereProductionController = TextEditingController();
  TextEditingController statusController = TextEditingController();
  TextEditingController descriptionsController = TextEditingController();

  void changeCategory(value) {
    categoryController = value;
    update();
  }

  Future<List<Yacht>?> fetchYachts() async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    final String? idBusiness = prefs.getString('idBusiness');
    try {
      Map<String, String> queryParams = {
        'idBusiness': idBusiness!,
      };

      final response = await http.get(
        Uri.parse(
                "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles")
            .replace(queryParameters: queryParams),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );

      // final response = await http.get(
      //   Uri.parse(
      //       "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles"),
      //   headers: {
      //     "Content-Type": "application/json",
      //     "Authorization": "Bearer $token"
      //   },
      // );

      // final uri = Uri.https("https://booking-yacht.azurewebsites.net", "/api/v1.0/business/vehicles", queryParams);
      // final response = await http.get(uri, headers: {
      //   HttpHeaders.authorizationHeader: 'Bearer $token',
      //   HttpHeaders.contentTypeHeader: 'application/json',
      // });
      print(response.statusCode);
      if (response.statusCode == 200) {
        var yachts = yachtReponseFromJson(response.body);
        print(response.body);
        if (yachts.data!.isNotEmpty) {
          listYacht = yachts.data as List<Yacht>;
        }
        getCategory();
        update();
        Get.to(Yachts());
      } else {
        return null;
      }
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return listYacht;
  }

  Future<List<Category>?> getCategory() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicle-types"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        var jsonString = response.body;
        var categorys = categoryReponseFromJson(jsonString);
        if (categorys.data.isNotEmpty) {
          listCategory = categorys.data;
        }
      } else {
        return null;
      }
    } catch (error) {
      print('loi r');
    }
    return listCategory;
  }

  Future<Yacht> getYachtDetail(String id) async {
    isLoading(true);
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.get(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/${id}"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token"
        },
      );
      if (response.statusCode == 200) {
        // var yachts = yachtReponseFromJson(response.body);
        // if (yachts.data != null) {
        //   yachtDetail = yachts.data as Yacht;
        // }
        var jsonString = json.decode(response.body);

        yachtDetail = Yacht(
            id: jsonString['data']['id'],
            name: jsonString['data']['name'],
            seat: jsonString['data']['seat'] as int,
            registrationNumber: jsonString['data']['registrationNumber'],
            yearOfManufacture: jsonString['data']['yearOfManufacture'] as int,
            whereProduction: jsonString['data']['whereProduction'],
            descriptions: jsonString['data']['descriptions'],
            idVehicleType: jsonString['data']['idVehicleType'],
            idBusiness: jsonString['data']['idBusiness'],
            status: jsonString['data']['status'] as int,
            imageLink: jsonString['data']['imageLink']);
        update();
        Get.to(YachtDetail());
      } else {
        // return null;
      }
    } catch (error) {
      print('loi r');
    } finally {
      isLoading(false);
    }
    return yachtDetail;
  }

  void editYacht(Yacht yacht) async {
    isAdding = false;
    id = yacht.id!;
    categoryController = yacht.idVehicleType!;
    nameController.text = yacht.name!;
    registrationNumberController.text = yacht.registrationNumber.toString();
    yearOfManufactureController.text = yacht.yearOfManufacture.toString();
    whereProductionController.text = yacht.whereProduction!;
    seatController.text = yacht.seat.toString();
    statusController.text = yacht.status.toString();
    descriptionsController.text = yacht.descriptions!;
    Get.to(YachtModify());
  }

  void addYacht() async {
    isAdding = true;
    nameController.clear();
    registrationNumberController.clear();
    yearOfManufactureController.clear();
    whereProductionController.clear();
    seatController.clear();
    statusController.clear();
    categoryController = "";
    descriptionsController.clear();
    Get.to(YachtModify());
  }

  void deleteYacht(String id) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      final response = await http.delete(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/${id}"),
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer $token",
          });
      if (response.statusCode == 200) {
        Get.back();
        Get.back();
        Fluttertoast.showToast(
            msg: "Xoá thành công",
            toastLength: Toast.LENGTH_SHORT,
            gravity: ToastGravity.BOTTOM,
            timeInSecForIosWeb: 1,
            backgroundColor: Colors.white,
            textColor: Colors.black,
            fontSize: 16.0);
        fetchYachts();
      } else {
        Fluttertoast.showToast(
            msg: "Lỗi rồi",
            toastLength: Toast.LENGTH_SHORT,
            gravity: ToastGravity.BOTTOM,
            timeInSecForIosWeb: 1,
            backgroundColor: Colors.white,
            textColor: Colors.black,
            fontSize: 16.0);
      }
    } catch (error) {
      print(error);
    }
  }

  void restoreYacht() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      Yacht yacht = Yacht(
          id: yachtDetail.id,
          name: yachtDetail.name,
          registrationNumber: yachtDetail.registrationNumber,
          yearOfManufacture: yachtDetail.yearOfManufacture,
          whereProduction: yachtDetail.whereProduction,
          seat: yachtDetail.seat,
          descriptions: yachtDetail.descriptions,
          idVehicleType: yachtDetail.idVehicleType,
          idBusiness: '68554b5a-817b-453c-992c-149662a8e710',
          status: 1);
      String body = json.encode(yacht);

      final response = await http.put(
        Uri.parse(
            "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/${yachtDetail.id}"),
        headers: {
          "Content-Type": "application/json",
          "Authorization": "Bearer $token",
        },
        body: body,
      );
      if (response.statusCode == 200) {
        Get.back();
        Get.back();
        Fluttertoast.showToast(
            msg: "Khôi phục thành công",
            toastLength: Toast.LENGTH_SHORT,
            gravity: ToastGravity.BOTTOM,
            timeInSecForIosWeb: 1,
            backgroundColor: Colors.white,
            textColor: Colors.black,
            fontSize: 16.0);
        fetchYachts();
      }
    } catch (error) {
      Fluttertoast.showToast(
          msg: "Lỗi rồi",
          toastLength: Toast.LENGTH_SHORT,
          gravity: ToastGravity.BOTTOM,
          timeInSecForIosWeb: 1,
          backgroundColor: Colors.white,
          textColor: Colors.white,
          fontSize: 16.0);
    }
  }

  void save() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    final String? idBusiness = prefs.getString('idBusiness');
    final isValid = yachtFormKey.currentState!.validate();
    if (!isValid) {
      return;
    }
    yachtFormKey.currentState!.save();
    try {
      if (!isAdding) {
        Yacht yacht = Yacht(
            id: id,
            name: nameController.text,
            registrationNumber: registrationNumberController.text,
            yearOfManufacture: int.parse(yearOfManufactureController.text),
            whereProduction: whereProductionController.text,
            seat: int.parse(seatController.text),
            descriptions: descriptionsController.text,
            idVehicleType: categoryController,
            idBusiness: idBusiness,
            status: int.parse(statusController.text),
            imageLink: imageLink);
        String body = json.encode(yacht);

        final response = await http.put(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/${id}"),
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer $token",
          },
          body: body,
        );
        if (response.statusCode == 200) {
          getYachtDetail(id);
          update();
          Get.back();
          Fluttertoast.showToast(
              msg: "Cập nhật thành công",
              toastLength: Toast.LENGTH_SHORT,
              gravity: ToastGravity.BOTTOM,
              timeInSecForIosWeb: 1,
              backgroundColor: Colors.white,
              textColor: Colors.black,
              fontSize: 16.0);
        } else {
          Fluttertoast.showToast(
              msg: "Lỗi rồi",
              toastLength: Toast.LENGTH_SHORT,
              gravity: ToastGravity.BOTTOM,
              timeInSecForIosWeb: 1,
              backgroundColor: Colors.white,
              textColor: Colors.black,
              fontSize: 16.0);
        }
      } else {
        String body = json.encode({
          'name': nameController.text,
          'seat': int.parse(seatController.text),
          'registrationNumber': registrationNumberController.text,
          'yearOfManufacture': int.parse(yearOfManufactureController.text),
          'whereProduction': whereProductionController.text,
          'descriptions': descriptionsController.text,
          'idVehicleType': categoryController,
          'idBusiness': idBusiness,
          'status': 1,
          'imageLink': imageLink
        });
        final response = await http.post(
          Uri.parse(
              "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles"),
          headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer $token",
          },
          body: body,
        );
        if (response.statusCode == 200) {
          Get.back();
          fetchYachts();
          Fluttertoast.showToast(
              msg: "Tạo mới thành công",
              toastLength: Toast.LENGTH_SHORT,
              gravity: ToastGravity.BOTTOM,
              timeInSecForIosWeb: 1,
              backgroundColor: Colors.white,
              textColor: Colors.black,
              fontSize: 16.0);
        } else {
          Fluttertoast.showToast(
              msg: "Lỗi rồi",
              toastLength: Toast.LENGTH_SHORT,
              gravity: ToastGravity.BOTTOM,
              timeInSecForIosWeb: 1,
              backgroundColor: Colors.white,
              textColor: Colors.black,
              fontSize: 16.0);
        }
      }
    } catch (error) {
      print(error);
    }
  }

  Future<void> uploadImage(File? image) async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    final String? token = prefs.getString('token');
    try {
      // String body =
      //     json.encode({'ImageFile': base64Encode(image!.readAsBytesSync())});

      // final response = await http.post(
      //   Uri.parse(
      //       "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/upload"),
      //   headers: {
      //     "Content-Type": "multipart/form-data",
      //     "Authorization": "Bearer $token",
      //   },
      //   body: {'ImageFile': base64Encode(image!.readAsBytesSync())},
      // );
      print(image!.path);
      var postUri = Uri.parse(
          "https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/upload");

      http.MultipartRequest request = http.MultipartRequest("POST", postUri);

      http.MultipartFile multipartFile =
          await http.MultipartFile.fromPath('ImageFile', image!.path);

      request.files.add(multipartFile);

      http.StreamedResponse response = await request.send();
      var responseString = await response.stream.bytesToString();
      imageLink = (json.decode(responseString))['data'];
      print(imageLink);
      //   var response = Uri.parse("https://booking-yacht.azurewebsites.net/api/v1.0/business/vehicles/upload");
      // var request = new http.MultipartRequest("POST", response);
      // request.fields['user'] = 'blah';
      // request.files.add(new http.MultipartFile.fromBytes('file', await File.fromUri("${image.path}").readAsBytes(), contentType: new MediaType('image', 'jpeg')))

      // request.send().then((response) {
      //   if (response.statusCode == 200) print("Uploaded!");
      // });
      //   print(response.statusCode);
      //   if (response.statusCode == 200) {
      //     Fluttertoast.showToast(
      //         msg: "Lưu thành công",
      //         toastLength: Toast.LENGTH_SHORT,
      //         gravity: ToastGravity.BOTTOM,
      //         timeInSecForIosWeb: 1,
      //         backgroundColor: Colors.white,
      //         textColor: Colors.black,
      //         fontSize: 16.0);
      //   } else {
      //     print('loi upload hinh');
      //     // Fluttertoast.showToast(
      //     //     msg: "Lỗi rồi",
      //     //     toastLength: Toast.LENGTH_SHORT,
      //     //     gravity: ToastGravity.BOTTOM,
      //     //     timeInSecForIosWeb: 1,
      //     //     backgroundColor: Colors.white,
      //     //     textColor: Colors.black,
      //     //     fontSize: 16.0);
      //   }
    } catch (error) {
      print(error);
    }
  }

  Future pickImage(bool isCamera) async {
    try {
      final image;
      if (!isCamera) {
        image = await ImagePicker().pickImage(source: ImageSource.gallery);
      } else {
        image = await ImagePicker().pickImage(source: ImageSource.camera);
      }
      if (image == null) {
        print('loi');
      } else {
        final imageTemporary = File(image.path);
        this.image = imageTemporary;
        uploadImage(this.image);
        print('oke');
      }
    } on PlatformException catch (e) {
      print('exception');
    }
  }

  String? validate(String value, String message) {
    if (value.isEmpty) {
      return message;
    }
    return null;
  }

  void cancel() {
    Get.back();
  }
}
