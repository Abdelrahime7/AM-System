
import 'package:amsfront/core/constants/endpoints.dart';
import 'package:amsfront/core/constants/token_Service.dart';
import 'package:amsfront/core/network/api_clients.dart';
import 'package:amsfront/features/SuperAdmin/data/repositories/dashboard_Repository.dart';
import 'package:amsfront/features/SuperAdmin/data/services/dashboard_service.dart';
import 'package:amsfront/features/SuperAdmin/presentation/stateManagement/dashboard_notifier.dart';
import 'package:amsfront/features/SuperAdmin/presentation/stateManagement/dashboard_stat.dart';
import 'package:amsfront/features/SuperAdmin/presentation/stateManagement/user_Manag_Notifier.dart';
import 'package:amsfront/features/SuperAdmin/presentation/stateManagement/user_Manag_Stat.dart';

import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:flutter_riverpod/legacy.dart';
import 'package:graphql_flutter/graphql_flutter.dart';




final apiClientProvider = Provider<ApiClient>((ref) {
  return ApiClient(Endpoints.devBaseUrl);
});

final graphQLClientProvider = Provider<GraphQLClient>((ref)
 { 
 final tokenService = TokenService();
  final httpLink = HttpLink(Endpoints.dashboardhttpLink); 
  
  final authLink = AuthLink(
     getToken: () async {
       final token = await tokenService.refreshTokenIfNeeded(); 
         print("Attaching token: $token"); //
     return token != null ? 'Bearer $token' : null; }, );
     
      final link = authLink.concat(httpLink); 
      return GraphQLClient( link: link, cache: 
      GraphQLCache(store: InMemoryStore()), 
      ); });

final dashboardServiceProvider = Provider<DashboardService>((ref) {
  return DashboardService(ref.watch(graphQLClientProvider)
  ,ref.watch(apiClientProvider)); // or inject dependencies here
});



final dashboardRepositoryProvider = Provider<DashboardRepository>((ref) {
  return DashboardRepository(ref.watch(dashboardServiceProvider));
});


final dashboardProvider =
    StateNotifierProvider<DashboardNotifier, DashboardState>(
        (ref) => DashboardNotifier(ref.watch(dashboardRepositoryProvider)));

//usemanagment
final userManagProvide=StateNotifierProvider<UserManagNotifier,UserManagState>((ref)=>
UserManagNotifier(ref.watch(dashboardRepositoryProvider)));