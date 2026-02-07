
import 'package:amsfront/features/register/presentation/widgets/welcome_section.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';


class Admin_infoScreen extends StatelessWidget {
  const Admin_infoScreen({super.key}); 
  @override
  Widget build(BuildContext context) {
      return AnnotatedRegion<SystemUiOverlayStyle>(
      value: const SystemUiOverlayStyle(
        statusBarColor: Color(0xFF111722),
        statusBarIconBrightness: Brightness.light,
      ),
    child: Scaffold( 
  body: SafeArea(
      child:
      Column(
        children: [
buildWelcomeSection("admin infos",'fil your infos'),
        ],
      )
      ,)

    ),
  

      );
  }


  
}