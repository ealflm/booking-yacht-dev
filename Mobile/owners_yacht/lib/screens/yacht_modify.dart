import 'package:flutter/material.dart';

class YachtModify extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('helloword'),
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
                decoration: InputDecoration(labelText: 'Name'),
                textInputAction: TextInputAction.next,
                onFieldSubmitted: (_) {},
                validator: (value) {},
                onSaved: (value) {},
              ),
              TextFormField(
                decoration: InputDecoration(labelText: 'Seat'),
                textInputAction: TextInputAction.next,
                keyboardType: TextInputType.number,
                onFieldSubmitted: (_) {},
                validator: (value) {},
                onSaved: (value) {},
              ),
              TextFormField(
                decoration: InputDecoration(labelText: 'Status'),
                validator: (value) {},
                onSaved: (value) {},
              ),
              TextFormField(
                decoration: InputDecoration(labelText: 'Descriptions'),
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
