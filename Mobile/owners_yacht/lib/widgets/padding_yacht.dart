import 'package:flutter/material.dart';

class YachtInformation extends StatelessWidget {
  final String title;
  final String txt;
  YachtInformation(this.title, this.txt);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(top: 15.0),
      child: Align(
          alignment: Alignment.centerLeft,
          child: Text('${title} : ${txt}',
              style: TextStyle(
                fontSize: 13,
              ))),
    );
  }
}
