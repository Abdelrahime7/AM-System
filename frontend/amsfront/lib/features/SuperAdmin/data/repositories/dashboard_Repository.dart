
import 'package:amsfront/features/SuperAdmin/data/services/dashboard_service.dart';

class DashboardRepository {
   DashboardService dashboardService ;
  DashboardRepository(this.dashboardService);


Future <Map<String, dynamic>> loadDashboardData() async {
try {
return await dashboardService.getDashboardData();
}
catch (e){rethrow;} 
}

}