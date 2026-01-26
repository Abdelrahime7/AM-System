
import 'package:amsfront/features/auth/data/models/user_model.dart';
import 'package:amsfront/features/auth/data/services/auth_service.dart';

class AuthRepository {
  final AuthService service;
  
  AuthRepository(this.service);

  Future<UserModel> login(String username, String password) async {
    try {
      return await service.login(username, password);
    } catch (e) {
      throw Exception("Login failed: $e");
    }
  }
}
