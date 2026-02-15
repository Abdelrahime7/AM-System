

import 'package:amsfront/app/enums/roles.dart';
import 'package:amsfront/app/enums/userStatus.dart';

class UserModel {
  final String accessToken;
  final String refreshToken;
  final roles role; // since backend returns role as int
  final UserStatus status;

  UserModel({
    required this.accessToken,
    required this.refreshToken,
    required this.role,
    required this.status,
  });

  factory UserModel.fromJson(Map<String, dynamic> json) {
    final tokens = json['tokens'] as Map<String, dynamic>;
    return UserModel(
      accessToken: tokens['accessToken'] as String,
      refreshToken: tokens['refreshToken'] as String,
      role: roles.values.byName(json['role']) , // ✅json['role'] ,
      status: UserStatus.values.byName(json['status']), // ✅json['status'] as String,
    );
  }
}
