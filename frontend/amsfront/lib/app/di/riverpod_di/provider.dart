
import 'package:amsfront/core/constants/endpoints.dart';
import 'package:amsfront/core/network/api_clients.dart';
import 'package:amsfront/features/SuperAdmin/data/repositories/dashboard_Repository.dart';
import 'package:amsfront/features/SuperAdmin/data/services/dashboard_service.dart';
import 'package:amsfront/features/SuperAdmin/presentation/stateManagement/dashboard_notifier.dart';
import 'package:amsfront/features/SuperAdmin/presentation/stateManagement/dashboard_stat.dart';

import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_riverpod/legacy.dart';




final apiClientProvider = Provider<ApiClient>((ref) {
  return ApiClient(Endpoints.devBaseUrl);
});


final dashboardServiceProvider = Provider<DashboardService>((ref) {
  return DashboardService(ref.watch(apiClientProvider)); // or inject dependencies here
});

final dashboardRepositoryProvider = Provider<DashboardRepository>((ref) {
  return DashboardRepository(ref.watch(dashboardServiceProvider));
});


final dashboardProvider =
    StateNotifierProvider<DashboardNotifier, DashboardState>(
        (ref) => DashboardNotifier(ref.watch(dashboardRepositoryProvider)));
