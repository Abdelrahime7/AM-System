
import 'package:dio/dio.dart';


class ApiClient {
  final Dio dio ;

  ApiClient(String baseUrl)
      : dio = Dio(BaseOptions(baseUrl: baseUrl));

  Future<Response> get(String endpoint) => dio.get(endpoint);
  Future<Response> post(String endpoint, {dynamic data}) =>
      dio.post(endpoint, data: data);
}
