import 'package:flutter/material.dart';
import 'package:owners_yacht/widgets/yacht_grid.dart';

class MainScreen extends StatefulWidget {
  const MainScreen({ Key? key }) : super(key: key);

  @override
  State<MainScreen> createState() => _MainScreenState();
}

class _MainScreenState extends State<MainScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('My Yacht'),
        // actions: <Widget>[
        //   PopupMenuButton(
        //     onSelected: (FilterOptions selectedValue) {
        //       setState(() {
        //         if (selectedValue == FilterOptions.Favorites) {
        //           _showOnlyFavorites = true;
        //         } else {
        //           _showOnlyFavorites = false;
        //         }
        //       });
        //     },
        //     icon: Icon(
        //       Icons.more_vert,
        //     ),
        //     itemBuilder: (_) => [
        //           PopupMenuItem(
        //             child: Text('Only Favorites'),
        //             value: FilterOptions.Favorites,
        //           ),
        //           PopupMenuItem(
        //             child: Text('Show All'),
        //             value: FilterOptions.All,
        //           ),
        //         ],
        //   ),
        //   Consumer<Cart>(
        //     builder: (_, cart, ch) => Badge(
        //           child: ch,
        //           value: cart.itemCount.toString(),
        //         ),
        //     child: IconButton(
        //       icon: Icon(
        //         Icons.shopping_cart,
        //       ),
        //       onPressed: () {
        //         Navigator.of(context).pushNamed(CartScreen.routeName);
        //       },
        //     ),
        //   ),
        // ],
      ),
      // drawer: AppDrawer(),
      body: YachtGrid(),
    );
    }
}