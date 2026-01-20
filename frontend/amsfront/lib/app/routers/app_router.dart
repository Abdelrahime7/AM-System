import 'package:amsfront/features/Splash/presentation/pages/splash_screen.dart';
import 'package:amsfront/features/auth/presentation/pages/login_screen.dart';
import 'package:flutter/material.dart';
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
      builder: (context, state) => Scaffold(
        appBar: AppBar(title: const Text('About Us')),
        body: const Center(child: Text('About Us Page')),
      ),
    ),
    GoRoute(
      path: '/works',
      builder: (context, state) => Scaffold(
        appBar: AppBar(title: const Text('Our Works')),
        body: const Center(child: Text('Our Works Page')),
      ),
    ),
  ],
);