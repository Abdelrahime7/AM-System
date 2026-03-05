

import 'package:amsfront/core/constants/endpoints.dart';
import 'package:amsfront/core/network/api_clients.dart';
import 'package:amsfront/features/register/data/model/register_Data.dart';
import 'package:dio/dio.dart';

class RegisterService {
 ApiClient apiClient;
 RegisterService(this.apiClient);



Future <void> register( RegisterPayload registerData) async{

final response = await apiClient.post(Endpoints.register,
data : registerData.toJson(),
options: Options(contentType: Headers.jsonContentType),

);
return response.data['success'];

}


}