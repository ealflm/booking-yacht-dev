import 'package:flutter/material.dart';

class NavBar extends StatefulWidget implements PreferredSizeWidget {
  final String title;
  final bool automaticallyImplyLeading;

  const NavBar(
      {Key? key, required this.title, required this.automaticallyImplyLeading})
      : super(key: key);

  @override
  State<NavBar> createState() => _NavBarState();

  final double _prefferedHeight = 50.0;
  @override
  // TODO: implement preferredSize
  Size get preferredSize => Size.fromHeight(_prefferedHeight);
}

class _NavBarState extends State<NavBar> {
  @override
  Widget build(BuildContext context) {
    return AppBar(
      title: Text(widget.title),
      backgroundColor: Colors.black,
      automaticallyImplyLeading: widget.automaticallyImplyLeading,
    );
  }
}
