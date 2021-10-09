import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/yacht.dart';

class YachtModify extends StatelessWidget {
  final YachtController controller = Get.find<YachtController>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(controller.nameController.text),
        actions: <Widget>[
          IconButton(
            icon: Icon(
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
                decoration: InputDecoration(labelText: 'Tên tàu'),
                textInputAction: TextInputAction.next,
                // onFieldSubmitted: (_) {},
                controller: controller.nameController,
                // validator: (value) {},
                // onSaved: (value) {},
              ),
              TextFormField(
                decoration: InputDecoration(labelText: 'Ghế'),
                textInputAction: TextInputAction.next,
                keyboardType: TextInputType.number,
                controller: controller.seatController,
              ),
              TextFormField(
                decoration: InputDecoration(labelText: 'Trạng thái'),
                validator: (value) {},
                onSaved: (value) {},
                controller: controller.statusController,
              ),
              Padding(
                padding: const EdgeInsets.all(8.0),
                child: Row(
                  children: [
                    FilterChip(
                      label: Text('Tàu thường'),
                      selected: false,
                      onSelected: (bool value) {},
                    ),
                    FilterChip(
                      label: Text('Tàu cùi'),
                      selected: false,
                      onSelected: (bool value) {},
                    ),
                    FilterChip(
                      label: Text('Tàu siêu cấp'),
                      selected: true,
                      onSelected: (bool value) {},
                    ),
                  ],
                ),
              ),
              TextFormField(
                decoration: InputDecoration(labelText: 'Mô tả'),
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
