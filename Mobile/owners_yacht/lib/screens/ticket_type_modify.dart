import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:owners_yacht/controller/ticket_type.dart';
import 'package:owners_yacht/controller/tour.dart';

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
          child: ListView(
            children: <Widget>[
              TextFormField(
                decoration: const InputDecoration(labelText: 'Loại vé'),
                textInputAction: TextInputAction.next,
                // onFieldSubmitted: (_) {},
                controller: _ticketTypeController.nameController,
                // validator: (value) {},
                // onSaved: (value) {},
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Giá'),
                textInputAction: TextInputAction.next,
                keyboardType: TextInputType.number,
                controller: _ticketTypeController.priceController,
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Trạng thái'),
                validator: (value) {},
                onSaved: (value) {},
                controller: _ticketTypeController.statusController,
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Phí hoa hồng(%)'),
                validator: (value) {},
                onSaved: (value) {},
                controller: _ticketTypeController.commissionFeeController,
              ),
              TextFormField(
                decoration: const InputDecoration(labelText: 'Phí dịch vụ(%)'),
                validator: (value) {},
                onSaved: (value) {},
                controller: _ticketTypeController.serviceFeeController,
              ),
              Padding(
                padding: const EdgeInsets.all(5.0),
                child: Wrap(
                  children: [
                    // DropdownButton<String>(
                    //   hint: Text(
                    //     'Chọn tour',
                    //     style: TextStyle(fontSize: 16),
                    //   ),
                    //   items: _tourController.listTour.map(
                    //     (e) => DropdownMenuItem<String>(
                    //       value: e.id,
                    //       child: Text(e.tittle),
                    //     ),
                    //   ),

                    // items: <String>[
                    //   'Aasdasdadasdddddddddddddddddd',
                    //   'Basdasdasd',
                    //   'asdasdsdC',
                    //   'asdasdD'
                    // ].map((String value) {
                    //   return DropdownMenuItem<String>(
                    //     value: value,
                    //     child: Text(value),
                    //   );
                    // }).toList(),
                    // onChanged: (_) {},
                    // )
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
