import 'package:amsfront/features/register/presentation/cubit/register_cubit.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart'; 
import 'package:amsfront/features/register/presentation/widgets/welcome_section.dart';
import 'package:amsfront/features/auth/presentation/widgets/_buildTextInput.dart';
import 'package:flutter_bloc/flutter_bloc.dart';



class Assisst_infoScreen extends StatelessWidget {
  const Assisst_infoScreen({super.key}); 
 
  
  @override
  Widget build(BuildContext context) {
      return AnnotatedRegion<SystemUiOverlayStyle>(
      value: const SystemUiOverlayStyle(
        statusBarColor: Colors.transparent,
        statusBarIconBrightness: Brightness.light,
      ),
     
    child: Scaffold( 
      backgroundColor: const Color(0xFF111722),

      extendBodyBehindAppBar: true,
      appBar: AppBar(
        backgroundColor: Colors.transparent,
        elevation: 0,
        leading: IconButton(
          icon: const Icon(Icons.arrow_back_ios_new_rounded, color: Colors.white),
          onPressed: () => Navigator.of(context).pop(),
        ),
      ),
      body: SafeArea(
          child: SingleChildScrollView(
            padding: const EdgeInsets.symmetric(horizontal: 24.0),
            child: Column(
              
              crossAxisAlignment: CrossAxisAlignment.stretch,
              children: [
                const SizedBox(height: 10),
                buildWelcomeSection("Assisstant Info", 'Enter your details below'),
                const SizedBox(height: 32),
                buildTextInput(
                  hint: 'Assigned by',
                  controller: TextEditingController(),
                ),
                const SizedBox(height: 20),

                const SizedBox(height: 32),
                SizedBox(
                  height: 56,
                  child: Container(
                    decoration: BoxDecoration(
                      gradient: const LinearGradient(
                        colors: [Color(0xFF2563EB), Color(0xFF1D4ED8)],
                      ),
                      borderRadius: BorderRadius.circular(12),
                      boxShadow: [
                        BoxShadow(
                          color: const Color(0xFF2563EB).withOpacity(0.3),
                          blurRadius: 12,
                          offset: const Offset(0, 4),
                        ),
                      ],
                    ),
                    child: ElevatedButton(
                      onPressed: () => context.read<RegisterCubit>().register(),
                      style: ElevatedButton.styleFrom(
                        backgroundColor: Colors.transparent,
                        shadowColor: Colors.transparent,
                        shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
                      ),
                      child: const Text(
                        'Register',
                        style: TextStyle(fontWeight: FontWeight.w700, fontSize: 16, color: Colors.white),
                      ),
                    ),
                  ),
                ),
                const SizedBox(height: 60),
                Center(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      Icon(
                        Icons.hub_rounded,
                        size: 80,
                        color: Colors.white.withOpacity(0.1),
                      ),
                      const SizedBox(height: 12),
                      Text(
                        "PARTNER NETWORK",
                        style: TextStyle(
                          color: Colors.white.withOpacity(0.1),
                          fontSize: 12,
                          fontWeight: FontWeight.bold,
                          letterSpacing: 2.0,
                        ),
                      ),
                    ],
                  ),
                ),
                const SizedBox(height: 20),
              ],
            ),
          ),
      ),
    ));
  }


  
}