import 'package:flutter/material.dart';
import 'package:owners_yacht/widgets/app_drawer.dart';

class ManagerYacht extends StatelessWidget {
  const ManagerYacht({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Add yacht'),
        actions: <Widget>[
          IconButton(
            icon: Icon(
              Icons.save_alt_rounded,
              color: Colors.white,
            ),
            onPressed: null,
          ),
        ],
        backgroundColor: Colors.black,
      ),
      drawer: AppDrawer(),
      body: Padding(
        padding: const EdgeInsets.all(16.0),
        child: Form(
          child: ListView(
            children: <Widget>[
              TextFormField(
                decoration: InputDecoration(labelText: 'Title'),
                textInputAction: TextInputAction.next,
                onFieldSubmitted: (_) {},
                validator: (value) {},
                onSaved: (value) {},
              ),
              TextFormField(
                decoration: InputDecoration(labelText: 'Price'),
                textInputAction: TextInputAction.next,
                keyboardType: TextInputType.number,
                onFieldSubmitted: (_) {},
                validator: (value) {},
                onSaved: (value) {},
              ),
              TextFormField(
                decoration: InputDecoration(labelText: 'Description'),
                validator: (value) {},
                onSaved: (value) {},
              ),
            ],
          ),
        ),
      ),
    );
  }
}
