import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/ticket_type.dart';
import 'package:owners_yacht/controller/tour.dart';
import 'package:owners_yacht/models/tour.dart';
import 'package:owners_yacht/models/yacht.dart';

class TicketTypeModify extends StatelessWidget {
  final TicketTypeController _ticketTypeController =
      Get.find<TicketTypeController>();
  final TourController _tourController = Get.find<TourController>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text((_ticketTypeController.isAdding == true)
            ? "Thêm mới"
            : _ticketTypeController.nameController.text),
        actions: <Widget>[
          IconButton(
            icon: const Icon(
              Icons.save_alt_rounded,
              color: Colors.white,
            ),
            onPressed: () => _ticketTypeController.save(),
          ),
        ],
        backgroundColor: Colors.black,
      ),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          key: _ticketTypeController.ticketTypeFormKey,
          autovalidateMode: AutovalidateMode.onUserInteraction,
          child: ListView(
            children: <Widget>[
              TextFormField(
                decoration: const InputDecoration(labelText: 'Tên loại vé'),
                textInputAction: TextInputAction.next,
                // onFieldSubmitted: (_) {},
                controller: _ticketTypeController.nameController,
                onSaved: (value) {
                  _ticketTypeController.nameController.text = value!;
                },
                validator: (value) {
                  return _ticketTypeController.validate(
                      value!, 'Vui lòng nhập tên loại vé');
                },
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Giá'),
                textInputAction: TextInputAction.next,
                keyboardType: TextInputType.number,
                controller: _ticketTypeController.priceController,
                onSaved: (value) {
                  _ticketTypeController.priceController.text = value!;
                },
                validator: (value) {
                  return _ticketTypeController.validate(
                      value!, 'Vui lòng nhập giá');
                },
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Phí hoa hồng(%)'),
                controller: _ticketTypeController.commissionFeeController,
                onSaved: (value) {
                  _ticketTypeController.commissionFeeController.text = value!;
                },
                validator: (value) {
                  return _ticketTypeController.validate(
                      value!, 'Vui lòng nhập phí hoa hồng');
                },
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Phí dịch vụ(%)'),
                onSaved: (value) {
                  _ticketTypeController.serviceFeeController.text = value!;
                },
                validator: (value) {
                  return _ticketTypeController.validate(
                      value!, 'Vui lòng nhập phí dịch vụ');
                },
                controller: _ticketTypeController.serviceFeeController,
              ),
              GetBuilder<TicketTypeController>(
                builder: (controller) => Padding(
                  padding: const EdgeInsets.all(5.0),
                  child: Wrap(
                    children: [
                      DropdownButton<String>(
                        isExpanded: true,
                        hint: const Text(
                          'Chọn tour',
                          style: TextStyle(fontSize: 16),
                        ),
                        value: controller.selectedValue,
                        items: _tourController.listTour.map((Tour value) {
                          return DropdownMenuItem<String>(
                            value: value.id,
                            child: Text(value.title!,
                                overflow: TextOverflow.ellipsis),
                          );
                        }).toList(),
                        onChanged: (newValue) {
                          controller.onSelected(newValue!);
                        },
                      )
                    ],
                  ),
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
