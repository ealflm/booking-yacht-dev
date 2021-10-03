import 'package:flutter/material.dart';

class Dropdown extends StatefulWidget {
  // const Dropdown({Key? key}) : super(key: key);

  @override
  State<Dropdown> createState() => _DropdownState();
}

class _DropdownState extends State<Dropdown> {
  String dropdownvalue = 'Tau 1';
  final items = [
    'Tau 1',
    'Tau 2',
    'Tau 3',
    'Tau 4',
  ];
  @override
  Widget build(BuildContext context) {
    return Container(
      child: Center(
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            DropdownButton(
              value: dropdownvalue,
              icon: Icon(Icons.keyboard_arrow_down),
              items: items.map((String items) {
                return DropdownMenuItem(value: items, child: Text(items));
              }).toList(),
              onChanged: (newValue) {
                setState(() {
                  dropdownvalue = newValue as String;
                });
              },
            ),
          ],
        ),
      ),
    );
  }
}
