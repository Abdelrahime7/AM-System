
import 'package:dio/dio.dart';


class ApiClient {
  final Dio dio;

  ApiClient(String baseUrl)
      : dio = Dio(BaseOptions(baseUrl: baseUrl));

  Future<Response> get(String endpoint) async => await dio.get(endpoint);
  Future<Response> post(String endpoint, {dynamic data}) async =>
      await dio.post(endpoint, data: data);
}
