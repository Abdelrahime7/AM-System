
class UserModel {
  final String accessToken;
  final String refreshToken;
  final int role; // since backend returns role as int

  UserModel({
    required this.accessToken,
    required this.refreshToken,
    required this.role,
  });

  factory UserModel.fromJson(Map<String, dynamic> json) {
    final tokens = json['tokens'] as Map<String, dynamic>;
    return UserModel(
      accessToken: tokens['accessToken'] as String,
      refreshToken: tokens['refreshToken'] as String,
      role: json['role'] as int,
    );
  }
}
