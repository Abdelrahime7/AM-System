import 'package:amsfront/core/constants/endpoints.dart';
import 'package:amsfront/core/network/api_clients.dart';

class DashboardService {
  ApiClient apiClient;

  DashboardService(this.apiClient);

  Future<Map<String, dynamic>> getDashboardData() async {
    
   final respons=  await apiClient.get(Endpoints.dashboard);
      
      return respons.data;
    
  }
}

