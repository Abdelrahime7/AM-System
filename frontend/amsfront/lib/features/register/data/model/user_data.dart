
// ignore: non_constant_identifier_names

import 'package:amsfront/app/enums/roles.dart';

class UserData{
  
 late String fullname ;
   late String phone ;
   late String email ;
   late String username ;
   late String password;  
   late String confirmPassword ;
   late roles role ;

   
 Map<String, dynamic> toJson() {
   return {
      "userName": username,
       "role": role.index,
        "fullName": fullname,
         "phone": phone,
          "email": email,
           "passwordHash":password,
            "status": 0
    } ;
 }
}

