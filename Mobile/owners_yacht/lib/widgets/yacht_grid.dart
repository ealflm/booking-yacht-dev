import 'package:flutter/material.dart';
import 'package:owners_yacht/providers/yachts.dart';
import 'package:provider/provider.dart';

import 'yacht_item.dart';

class YachtGrid extends StatelessWidget {

  @override
  Widget build(BuildContext context) {
    final data = Provider.of<Yachts>(context);
    final yachts = data.items;
    return GridView.builder(
      gridDelegate: SliverGridDelegateWithFixedCrossAxisCount(
        crossAxisCount: 2,
        childAspectRatio: 3 / 2,
        crossAxisSpacing: 10,
        mainAxisSpacing: 10,
      ),
      itemCount: yachts.length,
      itemBuilder: (BuildContext ctx, index) {
        return YachtItem(
          yachts[index].id,
          yachts[index].title,
          yachts[index].imageUrl,
        );
      },
    );
  }
}
