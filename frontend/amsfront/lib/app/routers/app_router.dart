import 'package:amsfront/features/Driver/presentation/pages/driverScreen.dart';
import 'package:amsfront/features/Splash/presentation/pages/aboutUs.dart';
import 'package:amsfront/features/Splash/presentation/pages/splash_screen.dart';
import 'package:amsfront/features/Splash/presentation/pages/our_works.dart';
import 'package:amsfront/features/Affiiates/presentation/pages/affilateScreen.dart';
import 'package:amsfront/features/Assisstant/presentation/pages/assisstantScreen.dart';
import 'package:amsfront/features/SuperAdmin/presentation/pages/superadminScreen.dart';
import 'package:amsfront/features/auth/presentation/pages/login_screen.dart';
import 'package:go_router/go_router.dart';

final GoRouter appRouter = GoRouter(
  initialLocation: '/',
  routes: [
    GoRoute(
      path: '/',
      builder: (context, state) => const SplashScreen(),
    ),
    GoRoute(
      path: '/login',
      builder: (context, state) => const LoginScreen(),
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
  ],
);