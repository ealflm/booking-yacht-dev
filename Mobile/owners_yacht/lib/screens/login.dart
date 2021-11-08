import 'package:flutter/material.dart';
import 'package:get/get.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
import '/controller/login.dart';

class Login extends StatelessWidget {
  final LoginController controller = Get.find<LoginController>();
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      backgroundColor: Colors.white,
      body: Container(
        decoration: BoxDecoration(
          image: DecorationImage(
            image: NetworkImage(
                'https://www.srmg.com/storage/our-brands/daH7SHHJYxNlZbnT1PfGfk7JPNWDSyuAxJyiolWg.jpg?fbclid=IwAR16AliC00V57ByZajmSMgF8vvwtJnLOA-2Il5sltLUEfaUdLP_qN7QnZ9I'),
            fit: BoxFit.cover,
          ),
        ),
        child: SafeArea(
          child: Padding(
            padding: const EdgeInsets.symmetric(horizontal: 32.0),
            child: Column(
              children: [
                const Padding(
                  padding: EdgeInsets.only(top: 80),
                  child: Text(
                    'Chủ Tàu',
                    style: TextStyle(
                        fontSize: 40,
                        fontWeight: FontWeight.bold,
                        color: Colors.white),
                  ),
                ),
                Spacer(),
                Padding(
                  padding: const EdgeInsets.only(bottom: 80.0),
                  child: Container(
                    width: MediaQuery.of(context).size.width,
                    child: ElevatedButton.icon(
                      style: ButtonStyle(
                          backgroundColor:
                              MaterialStateProperty.all<Color>(Colors.white70),
                          shape:
                              MaterialStateProperty.all<RoundedRectangleBorder>(
                                  RoundedRectangleBorder(
                                      borderRadius: BorderRadius.circular(18.0),
                                      side: BorderSide(color: Colors.white)))),
                      onPressed: () {
                        controller.login();
                      },
                      icon: FaIcon(FontAwesomeIcons.google),
                      label: const Text('Đăng nhập với Google'),
                    ),
                  ),
                )
              ],
            ),
          ),
        ),
      ),
    );
  }
}
