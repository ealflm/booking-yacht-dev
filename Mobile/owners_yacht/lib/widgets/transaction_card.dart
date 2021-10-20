import 'package:flutter/material.dart';
import 'package:owners_yacht/constants/Status.dart';
import 'package:owners_yacht/models/transaction.dart';

class TransactionCard extends StatelessWidget {
  final Transaction transaction;

  const TransactionCard({Key? key, required this.transaction})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Card(
      elevation: 2,
      child: ListTile(
        leading: CircleAvatar(
          backgroundImage: NetworkImage('https://cdn5.vectorstock.com/i/1000x1000/01/69/businesswoman-character-avatar-icon-vector-12800169.jpg'),
        ),
        title: Text('Tên đại lý: ${transaction.agencyName}'),
        subtitle: Column(
          children: [
            Text('Trạng thái: ${BookingYachtStatus.status[transaction.status].toString()}'),
            Text('Số lượng: ${transaction.quantityOfPerson}'),
          ],
        ),
        trailing: SizedBox(
          width: 100,
          child: Row(
            children: <Widget>[
              IconButton(
                onPressed: () {},
                icon: const Icon(
                  Icons.check,
                  color: Colors.greenAccent,
                ),
              ),
              IconButton(
                onPressed: () {},
                icon: const Icon(
                  Icons.cancel,
                  color: Colors.redAccent,
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}
