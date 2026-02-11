

import 'package:amsfront/app/enums/access_levels.dart';
import 'package:amsfront/features/register/data/model/user_data.dart';

abstract class RegisterPayload {
  Map<String, dynamic> toJson();
}

class AffiliateRegisterData implements RegisterPayload
{
 

  UserData userData;
  String referalcode;

 AffiliateRegisterData({
 required this.userData,
  required this.referalcode,
});

@override
Map<String, dynamic> toJson()
 { return {
 'userRequest': userData.toJson(),
  'roleRequest':{ 
   'referalCode': referalcode
   }, 

  }; 
  }

}

class AdminRegisterData {
 

  UserData userData;
  accesslevels accesslevel;


 AdminRegisterData({
 required this.userData,
  required this.accesslevel 
});

}

class DriverRegisterData {
 

  UserData userData;
  bool isLocal;
  bool isAvailable;


 DriverRegisterData({
 required this.userData,
  required this.isLocal, 
 required this.isAvailable,

});

}

class AssistantRegisterData  implements RegisterPayload {
   UserData userData;
   int assignedBy ;
  AssistantRegisterData({
 required this.userData,
  required this.assignedBy, 
       
});

  @override
  Map<String, dynamic> toJson() {
    return {
 'userRequest': userData.toJson(),
  'roleRequest':{ 
   'AssignedBy': assignedBy
         }
    };

  }
}

