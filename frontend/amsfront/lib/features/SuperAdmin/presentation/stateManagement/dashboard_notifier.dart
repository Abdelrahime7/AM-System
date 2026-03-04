import 'package:amsfront/features/SuperAdmin/data/repositories/dashboard_Repository.dart';
import 'package:amsfront/features/SuperAdmin/presentation/stateManagement/dashboard_stat.dart';
import 'package:state_notifier/state_notifier.dart';


class DashboardNotifier extends StateNotifier<DashboardState> {
  final DashboardRepository dashboardRepository;
  DashboardNotifier(this.dashboardRepository) : super(DashboardState());


  Future<void> loadDashboardData() async {
    try {
      state = state.copyWith(isLoading: true, error: null);

      final data = await dashboardRepository.loadDashboardData();
      state = state.copyWith(
        isLoading: false,
        totalSales: data.totalSales,
        activeAffiliates: data.activeAffiliates,
        pendingOrders: data.pendingOrders ,
        totalRevenue: data.totalRevenue,
        userRecent: data.userRecent,
        financRecent: data.financRecent,
        productRecent: data.productRecent,
      );
    } catch (e) {
      state = state.copyWith(isLoading: false, error: e.toString());
    }
  }

}

