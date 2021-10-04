import 'package:flutter/material.dart';
class ModifyYacht extends StatelessWidget {
  ModifyYacht(this.title) : super();
  final String title;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(title),
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
