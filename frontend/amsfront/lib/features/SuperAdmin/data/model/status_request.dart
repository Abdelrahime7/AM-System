import 'package:amsfront/app/enums/userStatus.dart';

class StatusRequest {
  int userID;
  UserStatus status;

  StatusRequest({required this.userID, required this.status});

  Map<String, dynamic> toJson() {
    return {
      'userID': userID, 
      'status': status.name,
    };
  
  }


}