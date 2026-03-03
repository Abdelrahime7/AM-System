import 'dart:convert';
import 'package:amsfront/core/constants/endpoints.dart';
import 'package:flutter_secure_storage/flutter_secure_storage.dart';
import 'package:http/http.dart' as http;

class TokenService {
  final storage = const FlutterSecureStorage();
 final Endpoints endpoints = Endpoints();


  // Decode JWT payload
  Map<String, dynamic> _decodePayload(String token) {
    final parts = token.split('.');
    if (parts.length != 3) throw Exception("Invalid token");
    final payload = utf8.decode(base64Url.decode(base64Url.normalize(parts[1])));
    return json.decode(payload);
  }

  // Check if token expired
  bool _isExpired(String token) {
    final payload = _decodePayload(token);
    final exp = payload['exp'] as int;
    final now = DateTime.now().millisecondsSinceEpoch ~/ 1000;
    return exp < now;
  }

  // Refresh token method
  Future<String?> refreshTokenIfNeeded() async {
    final accessToken = await storage.read(key: "accessToken");
    if (accessToken == null) return null;

    if (!_isExpired(accessToken)) {
      // Still valid
      return accessToken;
    }

    // Expired → refresh
    final refreshToken = await storage.read(key: "refreshToken");
    if (refreshToken == null) return null;

    final response = await http.post(
      Uri.parse("${Endpoints.devBaseUrl}${Endpoints.refresh}"),
      headers: {"Content-Type": "application/json"},
      body: json.encode({"refreshToken": refreshToken}),
    );

    if (response.statusCode == 200) {
      final data = json.decode(response.body);
      final newAccessToken = data["accessToken"];
      final newRefreshToken = data["refreshToken"];

      // Save new tokens
      await storage.write(key: "accessToken", value: newAccessToken);
      await storage.write(key: "refreshToken", value: newRefreshToken);

      return newAccessToken;
    } else {
      // Refresh failed → force logout
      await storage.delete(key: "accessToken");
      await storage.delete(key: "refreshToken");
      return null;
    }
  }
}
