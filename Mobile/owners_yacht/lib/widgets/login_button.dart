import 'package:flutter/material.dart';
import 'package:font_awesome_flutter/font_awesome_flutter.dart';
// import 'package:owners_yacht/providers/login_google.dart';
// import '/constants/Theme.dart';
// import 'package:provider/provider.dart';

class LoginGoogleButton extends StatelessWidget {
  const LoginGoogleButton({Key? key}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: const EdgeInsets.only(bottom: 80.0),
      child: Container(
          width: MediaQuery.of(context).size.width,
          child: ElevatedButton.icon(
              style: ButtonStyle(
                  shape: MaterialStateProperty.all<RoundedRectangleBorder>(
                      RoundedRectangleBorder(
                          borderRadius: BorderRadius.circular(18.0),
                          side: BorderSide(color: Colors.white)))),
              onPressed: () {
                // final provider =
                //     Provider.of<GoogleSignInProvider>(context, listen: false);
                // provider.googleLogin();
              },
              icon: FaIcon(FontAwesomeIcons.google),
              label: const Text('Login with Google'))),
    );
  }
}
