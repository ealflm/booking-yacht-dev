import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/constants/status.dart';
import 'package:owners_yacht/controller/yacht.dart';
import 'package:owners_yacht/widgets/radio_button.dart';

class YachtModify extends StatelessWidget {
  final YachtController controller = Get.find<YachtController>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text((controller.isAdding == true)
            ? "Thêm mới"
            : controller.nameController.text),
        actions: <Widget>[
          IconButton(
            icon: const Icon(
              Icons.save_alt_rounded,
              color: Colors.white,
            ),
            onPressed: () => controller.save(),
          ),
        ],
        backgroundColor: Colors.black,
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: controller.yachtFormKey,
          autovalidateMode: AutovalidateMode.onUserInteraction,
          child: ListView(
            children: <Widget>[
              TextFormField(
                decoration: const InputDecoration(labelText: 'Tên tàu'),
                textInputAction: TextInputAction.next,
                // onFieldSubmitted: (_) {},
                controller: controller.nameController,
                onSaved: (value) {
                  controller.nameController.text = value!;
                },
                validator: (value) {
                  return controller.validate(value!, 'Vui lòng nhập tên tàu');
                },
                // onSaved: (value) {},
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Biển số'),
                textInputAction: TextInputAction.next,
                controller: controller.registrationNumberController,
                onSaved: (value) {
                  controller.registrationNumberController.text = value!;
                },
                validator: (value) {
                  return controller.validate(value!, 'Vui lòng biển số');
                },
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Năm sản xuất'),
                textInputAction: TextInputAction.next,
                keyboardType: TextInputType.number,
                controller: controller.yearOfManufactureController,
                onSaved: (value) {
                  controller.yearOfManufactureController.text = value!;
                },
                validator: (value) {
                  return controller.validate(
                      value!, 'Vui lòng nhập năm sản xuất');
                },
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Nơi sản xuất'),
                textInputAction: TextInputAction.next,
                controller: controller.whereProductionController,
                onSaved: (value) {
                  controller.whereProductionController.text = value!;
                },
                validator: (value) {
                  return controller.validate(
                      value!, 'Vui lòng nhập nơi sản xuất');
                },
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Ghế'),
                textInputAction: TextInputAction.next,
                keyboardType: TextInputType.number,
                controller: controller.seatController,
                onSaved: (value) {
                  controller.seatController.text = value!;
                },
                validator: (value) {
                  return controller.validate(value!, 'Vui lòng nhập số ghế');
                },
              ),
              FlatButton(
                child: Text(
                  'Chọn hình từ camera',
                  style: TextStyle(fontSize: 20.0),
                ),
                onPressed: () => controller.pickImage(true),
              ),
              FlatButton(
                child: Text(
                  'Chọn hình từ thư viện',
                  style: TextStyle(fontSize: 20.0),
                ),
                onPressed: () => controller.pickImage(false),
              ),
              Padding(
                padding: const EdgeInsets.all(1.0),
                child: Wrap(
                  children: [
                    const Text(
                      'Chọn loại tàu',
                      style: TextStyle(fontSize: 16),
                    ),
                    GetBuilder<YachtController>(
                      builder: (controller) => Column(
                        children: [
                          SizedBox(
                            height: 150,
                            child: ListView.builder(
                              itemBuilder: (ctx, i) => RadioCategory(
                                  controller.listCategory[i].id,
                                  controller.listCategory[i].name,
                                  controller.categoryController),
                              itemCount: controller.listCategory.length,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ],
                ),
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Mô tả'),
                keyboardType: TextInputType.multiline,
                maxLines: 3,
                validator: (value) {},
                onSaved: (value) {},
                controller: controller.descriptionsController,
              ),
            ],
          ),
        ),
      ),
    );
  }
}
