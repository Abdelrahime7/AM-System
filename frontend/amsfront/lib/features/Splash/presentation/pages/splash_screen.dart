
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
        statusBarColor: Color(0xFF1A1F2E),
        statusBarIconBrightness: Brightness.light,
      ),
      child: Scaffold(
        backgroundColor: const Color(0xFF1A1F2E),
        body: Container(
          width: double.infinity,
          padding: const EdgeInsets.only(bottom: 104),
          child: Column(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [
            
                 const BmgLogo(),
                 const SizedBox(height: 48),
                 SizedBox(
                   width: 200,
                   child: ElevatedButton(
                     onPressed: () => context.push('/about'),
                     child: const Text('About Us'),
                   ),
                 ),
                 const SizedBox(height: 16),
                 SizedBox(
                   width: 200,
                   child: ElevatedButton(
                     onPressed: () => context.push('/works'),
                     child: const Text('Our Works'),
                   ),
                 ),
                 const SizedBox(height: 16),
                 SizedBox(
                   width: 200,
                   child: ElevatedButton(
                     onPressed: () => context.push('/login'),
                     child: const Text('Sign In'),
                   ),
                 ),
            ]
                ),
              ),
            
          ),
        );
    
  }
}
