
import 'package:amsfront/features/Splash/widgets/_bmglogo.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:go_router/go_router.dart';


class SplashScreen extends StatelessWidget {
  const SplashScreen({super.key});

 


  @override
  Widget build(BuildContext context) {
    return AnnotatedRegion<SystemUiOverlayStyle>(
      value: const SystemUiOverlayStyle(
        statusBarColor: Colors.transparent,
        statusBarIconBrightness: Brightness.light,
      ),
      child: Scaffold(
        body: Container(
          width: double.infinity,
          decoration: const BoxDecoration(
            gradient: LinearGradient(
              begin: Alignment.topCenter,
              end: Alignment.bottomCenter,
              colors: [
                Color(0xFF1A1F2E),
                Color(0xFF0F131C),
              ],
            ),
          ),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
              const BmgLogo(),
              const SizedBox(height: 60),
              const _MenuButton(title: 'About Us', route: '/about'),
              const SizedBox(height: 16),
              const _MenuButton(title: 'Our Works', route: '/works'),
              const SizedBox(height: 16),
              const _MenuButton(
                title: 'Sign In',
                route: '/login',
                isPrimary: true,
              ),
              const SizedBox(height: 48),
            ],
          ),
        ),
      ),
    );
  }
}

class _MenuButton extends StatelessWidget {
  final String title;
  final String route;
  final bool isPrimary;

  const _MenuButton({
    required this.title,
    required this.route,
    this.isPrimary = false,
  });

  @override
  Widget build(BuildContext context) {
    return SizedBox(
      width: 220,
      height: 50,
      child: ElevatedButton(
        style: ElevatedButton.styleFrom(
          backgroundColor: isPrimary ? const Color(0xFF2563EB) : Colors.white.withOpacity(0.05),
          foregroundColor: Colors.white,
          elevation: isPrimary ? 8 : 0,
          shadowColor: isPrimary ? const Color(0xFF2563EB).withOpacity(0.5) : null,
          side: isPrimary ? BorderSide.none : const BorderSide(color: Colors.white24),
          shape: RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(16),
          ),
        ),
        onPressed: () => context.push(route),
        child: Text(
          title,
          style: const TextStyle(
            fontSize: 16,
            fontWeight: FontWeight.w600,
            letterSpacing: 0.5,
          ),
        ),
      ),
    );
  }
}
