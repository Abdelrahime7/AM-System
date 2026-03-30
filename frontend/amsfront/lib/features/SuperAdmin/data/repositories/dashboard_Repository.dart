
import 'package:amsfront/features/SuperAdmin/data/model/dashboardModel.dart';
import 'package:amsfront/features/SuperAdmin/data/model/status_request.dart';
import 'package:amsfront/features/SuperAdmin/data/model/user_model.dart';
import 'package:amsfront/features/SuperAdmin/data/services/dashboard_service.dart';

class DashboardRepository {
   DashboardService dashboardService ;
   
  DashboardRepository(this.dashboardService);
  


Future<DashboardModel> loadDashboardData() {
  return dashboardService.getDashboardData();
}

Future<List<UserResponse>> getUsers() {
  return dashboardService.getUsers();
}

Future<void> changeUserStatus(StatusRequest stutus) {
  return dashboardService.changeUserStatus(stutus);
}
}
