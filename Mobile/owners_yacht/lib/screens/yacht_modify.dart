import 'package:flutter/material.dart';
import 'package:get/get.dart';
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
          child: ListView(
            children: <Widget>[
              TextFormField(
                decoration: const InputDecoration(labelText: 'Tên tàu'),
                textInputAction: TextInputAction.next,
                // onFieldSubmitted: (_) {},
                controller: controller.nameController,
                // validator: (value) {},
                // onSaved: (value) {},
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Ghế'),
                textInputAction: TextInputAction.next,
                keyboardType: TextInputType.number,
                controller: controller.seatController,
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Trạng thái'),
                validator: (value) {},
                onSaved: (value) {},
                controller: controller.statusController,
              ),
              Padding(
                padding: const EdgeInsets.all(5.0),
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
                            height: 250,
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
