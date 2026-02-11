import 'package:amsfront/features/Driver/presentation/pages/driverScreen.dart';
import 'package:amsfront/features/Splash/presentation/pages/aboutUs.dart';
import 'package:amsfront/features/Splash/presentation/pages/splash_screen.dart';
import 'package:amsfront/features/Splash/presentation/pages/our_works.dart';
import 'package:amsfront/features/Affiiates/presentation/pages/affilateScreen.dart';
import 'package:amsfront/features/Assisstant/presentation/pages/assisstantScreen.dart';
import 'package:amsfront/features/SuperAdmin/presentation/pages/superadminScreen.dart';
import 'package:amsfront/features/auth/presentation/pages/login_screen.dart';
import 'package:amsfront/features/register/data/model/user_data.dart';
import 'package:amsfront/features/register/presentation/pages/admin_info_screen.dart';
import 'package:amsfront/features/register/presentation/pages/affiliate_info_screen.dart';
import 'package:amsfront/features/register/presentation/pages/assisstant_info_screen.dart';
import 'package:amsfront/features/register/presentation/pages/driver_info_screen.dart';
import 'package:amsfront/features/register/presentation/pages/register_screen.dart';
import 'package:amsfront/features/register/presentation/cubit/register_cubit.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:go_router/go_router.dart';

final GoRouter appRouter = GoRouter(
  initialLocation: '/',
  routes: [
    GoRoute(
      path: '/',
      builder: (context, state) => const SplashScreen(),
    ),
    GoRoute(
      path: '/register',
      builder: (context, state) => const  RegisterScreen()
    ),
    GoRoute(
      path: '/login',
      builder: (context, state) => LoginScreen()
    ),
    GoRoute(
      path: '/about',
      builder: (context, state) =>const AboutUsScreen()
      ),
    
    GoRoute(
      path: '/works',
      builder: (context, state) => const OurWorksScreen()
  
    ),
     GoRoute(
      path: '/SuperAdmin',
      builder: (context, state) => const SuperAdminScreen(),
    ),
    GoRoute(
      path: '/Affiliate',
      builder: (context, state) => const AffiliateScreen(),
    ),
    GoRoute(
      path: '/Assisstant',
      builder: (context, state) => const AssistantScreen(),
    ),
    GoRoute(
      path: '/Driver',
      builder: (context, state) => const DriverScreen(),
    ),
  
GoRoute(
  path: '/affiliate-info',
  builder: (context, state) {
    final extra = state.extra;

    if (extra is UserData) {
      return Affiliat_infoScreen(
        selectedRole: 'Affiliate',
        userData: extra,
      );
    }

    // fallback if no userData passed
    return const RegisterScreen();
  },
),


    
    GoRoute(path: '/Admin-info',
      builder: (context, state) {
        final extra = state.extra;
        if (extra is RegisterCubit) {
          return BlocProvider.value(
            value: extra,
            child: const Admin_infoScreen(),
          );
        }
        return const RegisterScreen();
      },
    ),

     GoRoute(path: '/Assisstant-info',
      builder: (context, state) {
        final extra = state.extra;

    if (extra is UserData) {
      return Assisstant_infoScreen(
        selectedRole: 'Assisstant',
        userData: extra,
      );
       
       
        }
        return const RegisterScreen();
      },
    ),
     GoRoute(path: '/Driver-info',
      builder: (context, state) {
        final extra = state.extra;
        if (extra is RegisterCubit) {
          return BlocProvider.value(
            value: extra,
            child: const Driver_infoScreen(),
          );
        }
        return const RegisterScreen();
      },
    ),
  ],

);