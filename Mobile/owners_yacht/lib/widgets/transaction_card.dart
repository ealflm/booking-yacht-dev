import 'package:flutter/material.dart';
import 'package:owners_yacht/constants/Status.dart';
import 'package:owners_yacht/models/transaction.dart';
import 'package:flutter_slidable/flutter_slidable.dart';

class TransactionCard extends StatelessWidget {
  final Transaction transaction;

  const TransactionCard({Key? key, required this.transaction})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Slidable(
      actionPane: SlidableDrawerActionPane(),
      actionExtentRatio: 0.25,
      child: Card(
        elevation: 2,
        child: ListTile(
          leading: CircleAvatar(
            backgroundImage: NetworkImage(
                'https://cdn5.vectorstock.com/i/1000x1000/01/69/businesswoman-character-avatar-icon-vector-12800169.jpg'),
          ),
          title: Text('Tên đại lý: ${transaction.agencyName}'),
          subtitle: Column(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            crossAxisAlignment: CrossAxisAlignment.start,
            children: [
              Text(
                  'Trạng thái: ${BookingYachtStatus.status[transaction.status].toString()}'),
              Text('Số lượng: ${transaction.quantityOfPerson}'),
            ],
          ),
        ),
      ),
      secondaryActions: <Widget>[
        IconSlideAction(
          caption: 'Chấp nhận',
          color: Colors.green,
          icon: Icons.check,
          onTap: () => print('say hello'),
        ),
        IconSlideAction(
          caption: 'Xem thêm',
          color: Colors.black45,
          icon: Icons.more_horiz,
          onTap: () => print('hello'),
        ),
      ],
    );
  }
}
