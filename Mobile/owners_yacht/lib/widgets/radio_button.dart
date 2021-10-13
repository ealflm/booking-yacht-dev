import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/yacht.dart';

class RadioCategory extends StatelessWidget {
  String title;
  String groupValue;

  RadioCategory(this.title, this.groupValue);

  YachtController controller = Get.find<YachtController>();
  @override
  Widget build(BuildContext context) {
    return RadioListTile<String>(
      title: Text(title),
      value: title,
      groupValue: groupValue,
      onChanged: (String? value) => controller.changeCategory(value),
    );
  }
}
