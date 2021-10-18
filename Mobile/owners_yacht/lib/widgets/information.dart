import 'package:flutter/material.dart';

class YachtInformation extends StatelessWidget {
  final String txtKey;
  final String value;
  YachtInformation(this.txtKey, this.value);

  @override
  Widget build(BuildContext context) {
    return Text('${txtKey}: ${value}',
        style: const TextStyle(
            color: Colors.black, fontSize: 14, fontWeight: FontWeight.w600));
  }
}
