import 'dart:io';
import 'package:dio/dio.dart';
// ignore: depend_on_referenced_packages
import 'package:dio/io.dart';

class ApiClient {
  final Dio dio;

  ApiClient(String baseUrl) : dio = Dio(BaseOptions(baseUrl: baseUrl)) {
    // THIS IS FOR DEVELOPMENT ONLY!
    // It allows connecting to a local server with a self-signed certificate.
    // Do NOT use this in production.
    (dio.httpClientAdapter as IOHttpClientAdapter).createHttpClient = () {
      final client = HttpClient();
      client.badCertificateCallback =
          (X509Certificate cert, String host, int port) => true;
      return client;
    };
  }

Future<Response> get(String endpoint) => dio.get(endpoint);
  Future<Response> post(String endpoint, {dynamic data, required Options options}) =>
      dio.post(endpoint, data: data);
}
