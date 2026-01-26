import 'package:amsfront/core/constants/endpoints.dart';
import 'package:get_it/get_it.dart';

import 'package:amsfront/core/network/api_clients.dart';
import 'package:amsfront/features/auth/data/repositories/auth_repository.dart';
import 'package:amsfront/features/auth/data/services/auth_service.dart';
import 'package:amsfront/features/auth/presentation/cubit/login_cubit.dart';


final getIt = GetIt.instance;

void setupDependencies() {
  // Core
  getIt.registerLazySingleton(() => ApiClient(Endpoints.devBaseUrl));

  // Services
  getIt.registerLazySingleton(() => AuthService(getIt<ApiClient>()));

  // Repositories
  getIt.registerLazySingleton(() => AuthRepository(getIt<AuthService>()));

  // State (Bloc/Provider)
  getIt.registerFactory(() => LoginCubit(getIt<AuthRepository>()));
}
