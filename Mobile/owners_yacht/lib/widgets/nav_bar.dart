import 'package:flutter/material.dart';
import 'package:owners_yacht/constants/theme.dart';

class NavBar extends StatefulWidget implements PreferredSizeWidget {
  final String title;
  final Color color;
  final bool automaticallyImplyLeading;

  const NavBar(
      {Key? key,
      required this.title,
      this.color = BookingYachtColors.appBar,
      this.automaticallyImplyLeading = true})
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
      backgroundColor: widget.color,
      automaticallyImplyLeading: widget.automaticallyImplyLeading,
    );
  }
}
