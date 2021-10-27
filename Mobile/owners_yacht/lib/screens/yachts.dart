import 'package:flutter/material.dart';
import 'package:get/instance_manager.dart';
import '/controller/yacht.dart';
import '/widgets/yacht_card.dart';
import 'yacht_modify.dart';
import '../widgets/nav_bar.dart';
import '../widgets/yacht_card.dart';
import 'package:get/get.dart';

class Yachts extends StatelessWidget {
  YachtController controller = Get.find<YachtController>();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: const NavBar(
        title: 'Tàu',
        automaticallyImplyLeading: true,
      ),
      backgroundColor: Colors.grey[200],
      body: GetBuilder<YachtController>(
        builder: (controller) => (controller.isLoading.isTrue)
            ? const Center(child: CircularProgressIndicator())
            : controller.listYacht.isEmpty
                ? const Center(child: Text('Không có tàu nào!'))
                : Padding(
                    padding: const EdgeInsets.only(top: 10.0),
                    child: ListView.builder(
                      itemBuilder: (ctx, i) =>
                          YachtCard(controller.listYacht[i]),
                      itemCount: controller.listYacht.length,
                    ),
                  ),
      ),
      floatingActionButton: FloatingActionButton(
        tooltip: 'Add',
        child: Icon(Icons.add, color: Colors.black),
        onPressed: () => controller.addYacht(),
        backgroundColor: Colors.white,
      ),
    );
  }
}











// class Yachts extends StatelessWidget {
//   List<Yacht> _list = [
//     Yacht(
//       id: 'a',
//       title: 'Tau du lich 1',
//       description: 'Tau du lich 1Tau du lich 1',
//       price: 29.99,
//       status: 'available',
//       imageUrl:
//           'https://www.ferretti-yachts.com/uploadB2B/Models/Images/Main/Ferretti/medium/47591.jpg',
//     ),
//     Yacht(
//       id: 'b',
//       title: 'Tau du lich 2',
//       description: 'Tau du lich 1Tau du lich 2',
//       price: 59.99,
//       status: 'available',
//       imageUrl:
//           'https://i1.wp.com/www.barcheamotore.com/wp-content/uploads/2019/10/Ferretti-Yachts-720_1.jpg?fit=900%2C500&ssl=1',
//     ),
//   ];

  // @override
  // Widget build(BuildContext context) {
  //   return Scaffold();
  // }
//       appBar: NavBar(
//         title: 'Home',
//       ),
//       body: Padding(
//         padding: const EdgeInsets.all(8.0),
//         child: ListView.builder(
//           itemBuilder: (ctx, i) =>
//               YachtCard(_list[i].title, _list[i].status, _list[i].imageUrl),
//           itemCount: _list.length,
//         ),
//       ),
//       floatingActionButton: FloatingActionButton(
//         tooltip: 'Add',
//         child: Icon(Icons.add, color: Colors.black),
//         onPressed: () {
//           Navigator.push(
//               context,
//               MaterialPageRoute(
//                   builder: (context) => ModifyYacht('Add yacht')));
//         },
//         backgroundColor: Colors.white,
//       ),
//     );
//   }
// }
