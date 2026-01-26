import 'package:amsfront/core/network/api_clients.dart';
import 'package:amsfront/features/auth/data/models/user_model.dart';
import 'package:amsfront/core/constants/endpoints.dart';


class AuthService {
  final ApiClient apiClient;
  AuthService(this.apiClient);

  Future<UserModel> login(String username, String password) async {
    final response = await apiClient.post(
      Endpoints.login,
      data: {"username": username, "password": password},
    );
    return UserModel.fromJson(response.data);
  }
}
