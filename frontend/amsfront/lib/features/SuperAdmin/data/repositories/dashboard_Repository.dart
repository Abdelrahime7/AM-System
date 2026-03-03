
import 'package:amsfront/features/SuperAdmin/data/model/dashboardModel.dart';
import 'package:amsfront/features/SuperAdmin/data/services/dashboard_service.dart';

class DashboardRepository {
   DashboardService dashboardService ;
   
  DashboardRepository(this.dashboardService);
  


Future <DashboardModel> loadDashboardData() async {
try {

return await dashboardService.getDashboardData();
}
catch (e){rethrow;} 
}

}