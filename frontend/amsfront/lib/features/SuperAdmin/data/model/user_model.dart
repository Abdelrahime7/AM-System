
import 'package:amsfront/app/enums/userStatus.dart';

class UserResponse {
final int id;
final String username ;
final  String role ;
final  String fullName ;
final  String phone ;
final  String email ;
final UserStatus status ;
 final String? lastLoginAt;

    UserResponse({ required this.username, required this.role, required this.fullName,
     required this.phone, required this.email, required this.status, this.lastLoginAt, required this.id});


factory UserResponse.fromJson(Map<String, dynamic> json) {

return UserResponse
(
  id: json['id'] ?? 0,
  username: json['username']  ?? '',
  role: json['role']  ?? '',
  fullName: json['fullName'] ?? '', 
  phone: json['phone']  ?? '',
  email: json['email']  ?? '',
   status: UserStatus.values.firstWhere(
        (e) => e.name.toLowerCase() == (json['status'] ?? '').toString().toLowerCase(),
        orElse: () => UserStatus.Pending, // fallback
      ),
   lastLoginAt: json['lastLoginAt']?.toString(),
);
}

  UserResponse copyWith({
    int? id,
    String? username,
    String? role,
    String? fullName,
    String? phone,
    String? email,
    UserStatus? status,
    String? lastLoginAt,
  }) {
    return UserResponse(
      id: id ?? this.id,
      username: username ?? this.username,
      role: role ?? this.role,
      fullName: fullName ?? this.fullName,
      phone: phone ?? this.phone,
      email: email ?? this.email,
      status: status ?? this.status,
      lastLoginAt: lastLoginAt ?? this.lastLoginAt,
    );
  }
            
}