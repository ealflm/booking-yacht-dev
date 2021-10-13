import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/yacht.dart';

class RadioCategory extends StatelessWidget {
  String categoryName;
  String groupValue;
  String categoryID;

  RadioCategory(this.categoryID, this.categoryName, this.groupValue);

  YachtController controller = Get.find<YachtController>();
  @override
  Widget build(BuildContext context) {
    return RadioListTile<String>(
      title: Text(categoryName),
      value: categoryID,
      groupValue: groupValue,
      onChanged: (String? value) => controller.changeCategory(value),
    );
  }
}
