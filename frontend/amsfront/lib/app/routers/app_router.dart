import 'package:amsfront/app/enums/roles.dart';
import 'package:amsfront/features/Driver/presentation/pages/driverScreen.dart';
import 'package:amsfront/features/Splash/presentation/pages/aboutUs.dart';
import 'package:amsfront/features/Splash/presentation/pages/splash_screen.dart';
import 'package:amsfront/features/Splash/presentation/pages/our_works.dart';
import 'package:amsfront/features/Affiiates/presentation/pages/affilateScreen.dart';
import 'package:amsfront/features/Assisstant/presentation/pages/assisstantScreen.dart';
import 'package:amsfront/features/SuperAdmin/presentation/pages/superadminScreen.dart';
import 'package:amsfront/features/auth/presentation/pages/aproval_waiting.dart';
import 'package:amsfront/features/auth/presentation/pages/login_screen.dart';
import 'package:amsfront/features/register/data/model/user_data.dart';
import 'package:amsfront/features/register/presentation/pages/admin_info_screen.dart';
import 'package:amsfront/features/register/presentation/pages/affiliate_info_screen.dart';
import 'package:amsfront/features/register/presentation/pages/assisstant_info_screen.dart';
import 'package:amsfront/features/register/presentation/pages/driver_info_screen.dart';
import 'package:amsfront/features/register/presentation/pages/register_screen.dart';
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
    GoRoute(path: ("/waiting"),
    builder: (context, state) => const AprovaL_Waiting()
    ),
  
GoRoute(
  path: '/affiliate-info',
  builder: (context, state) {
    final extra = state.extra;

    if (extra is UserData) {
      return Affiliat_infoScreen(
        selectedRole: roles.Affiliate.name,
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
        if (extra is UserData) {
          return Admin_infoScreen(
           selectedRole: roles.Admin.name,
           userData: extra,
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
        selectedRole:  roles.Assistant.name,
        userData: extra,
      );
       
       
        }
        return const RegisterScreen();
      },
    ),
     GoRoute(path: '/Driver-info',
      builder: (context, state) {
        final extra = state.extra;
        if (extra is UserData) {
          return  Driver_infoScreen(
 selectedRole: roles.Driver.name,
        userData: extra,
          
          );
        }
    return const RegisterScreen();
      },
    ),
    
  ],
);
  
