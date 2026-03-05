import 'package:amsfront/core/constants/endpoints.dart';
import 'package:amsfront/features/register/data/model/register.dart';
import 'package:amsfront/features/register/data/model/user_data.dart';
import 'package:amsfront/features/register/data/repositories/register_repository.dart';
import 'package:amsfront/features/register/data/service/register_service.dart';
import 'package:amsfront/features/register/presentation/cubit/register_cubit.dart';
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
  getIt.registerLazySingleton(() => RegisterService(getIt<ApiClient>()));


  // Repositories
  getIt.registerLazySingleton(() => AuthRepository(getIt<AuthService>()));
  getIt.registerLazySingleton(() => RegisterRepository(getIt<RegisterService>()));


  // State (Bloc/Provider)
  getIt.registerFactory(() => LoginCubit(getIt<AuthRepository>()));

 getIt.registerLazySingleton<AffiliateRegister>(
  () => AffiliateRegister(getIt<RegisterRepository>()),
);

 
 getIt.registerLazySingleton<AssisstantRegister>(
  () => AssisstantRegister(getIt<RegisterRepository>()),
);


 getIt.registerLazySingleton<AdminRegister>(
  () => AdminRegister(getIt<RegisterRepository>()),
);

getIt.registerLazySingleton<Driveregister>(
  () => Driveregister(getIt<RegisterRepository>()),
);
}

void factor(String  selectedRole ,UserData userData) {
  switch (selectedRole) {
     case   "Admin" : 
      if (getIt.isRegistered<RegisterCubit>())
       {
         getIt.unregister<RegisterCubit>();
        }
        getIt.registerFactory<RegisterCubit>(
          ()=> RegisterCubit(getIt<AdminRegister>(), userData),
         ); break;
    
      case 'Affiliate': 
      if (getIt.isRegistered<RegisterCubit>())
       {
         getIt.unregister<RegisterCubit>();
        }
        getIt.registerFactory<RegisterCubit>(
          ()=> RegisterCubit(getIt<AffiliateRegister>(), userData),
         ); break;

         case 'Assisstant': 
      if (getIt.isRegistered<RegisterCubit>())
       {
         getIt.unregister<RegisterCubit>();
        }
        getIt.registerFactory<RegisterCubit>(
          ()=> RegisterCubit(getIt<AssisstantRegister>(), userData),
         ); break;

            case 'Driver': 
      if (getIt.isRegistered<RegisterCubit>())
       {
         getIt.unregister<RegisterCubit>();
        }
        getIt.registerFactory<RegisterCubit>(
          ()=> RegisterCubit(getIt<Driveregister>(), userData),
         ); break;


      default :
     // getIt.registerFactory(()=>RegisterCubit(getIt<Roleregister>(),userData));
      
  }
}


