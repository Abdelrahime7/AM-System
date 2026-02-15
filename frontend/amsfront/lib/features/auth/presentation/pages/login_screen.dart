import 'package:amsfront/app/di/injector/injectors.dart';
import 'package:amsfront/app/enums/roles.dart';
import 'package:amsfront/app/enums/userStatus.dart';
import 'package:amsfront/app/switcher.dart';
import 'package:amsfront/features/auth/presentation/cubit/login_cubit.dart';
import 'package:amsfront/features/auth/presentation/cubit/login_state.dart';
import 'package:amsfront/features/register/presentation/widgets/text_input.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:go_router/go_router.dart';



// ignore: must_be_immutable
class LoginScreen extends StatelessWidget {
  const LoginScreen({super.key});
  

  @override
  Widget build(BuildContext context) {
    

    return AnnotatedRegion<SystemUiOverlayStyle>(
      value: const SystemUiOverlayStyle(
        statusBarColor: Color(0xFF111722),
        statusBarIconBrightness: Brightness.light,
        statusBarBrightness: Brightness.dark,
      ),
      child: BlocProvider(
        create: (context) =>getIt<LoginCubit>(),
        child: BlocConsumer<LoginCubit, LoginState>(
          listener: (context, state) {
            if (state is LoginSuccess) {
              ScaffoldMessenger.of(context).showSnackBar(
                SnackBar(content: Text(state.message)),
              );
              // Navigate t final UserStatus _status =context.read<LoginCubit>().status;
    final roles _role =context.read<LoginCubit>().role;
    final UserStatus _status =context.read<LoginCubit>().status;
           
             if(_status == UserStatus.Pending) {
                context.push("/waiting");
                return;
              }       
          
            switcher.routing(context,_role);
            
            } else if (state is LoginFailure) {
              ScaffoldMessenger.of(context).showSnackBar(
                SnackBar(
                  content: Text(state.error),
                  backgroundColor: Colors.red,
                ),
              );
            }
          },
          builder: (context, state) {
            if (state is LoginLoading) {
              return const Scaffold(
                backgroundColor: Color(0xFF111722),
                body: Center(child: CircularProgressIndicator()),
              );
            }
            return Scaffold(
              backgroundColor: const Color(0xFF111722),
              body: SafeArea(
                child: SingleChildScrollView(
                  padding: const EdgeInsets.only(bottom: 104),
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.stretch,
                    children: [
                      // Header
                      Padding(
                        padding: const EdgeInsets.fromLTRB(24, 24, 24, 40),
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            // Logo Container
                            const SizedBox(
                              width: 107,
                              height: 28,
                              // child: Placeholder(color: Colors.white), // Add logo here
                            ),
                            // Language Button
                            InkWell(
                              onTap: () {
                                debugPrint('Language toggle pressed');
                              },
                              borderRadius: BorderRadius.circular(9999),
                              child: Container(
                                width: 83,
                                height: 38,
                                decoration: BoxDecoration(
                                  border: Border.all(color: const Color(0xFF475569)),
                                  borderRadius: BorderRadius.circular(9999),
                                ),
                                alignment: Alignment.center,
                                child: const Text(
                                  'EN / AR',
                                  style: TextStyle(
                                    fontWeight: FontWeight.w500,
                                    fontSize: 14,
                                    color: Colors.white,
                                  ),
                                ),
                              ),
                            ),
                          ],
                        ),
                      ),

                      // Main Content
                      Padding(
                        padding: const EdgeInsets.symmetric(horizontal: 24),
                        child: Column(
                          children: [
                            // Welcome Section
                            const SizedBox(height: 54),
                            const Text(
                              'Welcome Back',
                              textAlign: TextAlign.center,
                              style: TextStyle(
                                fontWeight: FontWeight.w700,
                                fontSize: 30,
                                color: Colors.white,
                                height: 1.2,
                              ),
                            ),
                            const SizedBox(height: 8),
                            const Text(
                              'Sign in to continue',
                              textAlign: TextAlign.center,
                              style: TextStyle(
                                fontWeight: FontWeight.w400,
                                fontSize: 16,
                                color: Color(0xFF94A3B8),
                                height: 1.5,
                              ),
                            ),
                            const SizedBox(height: 40),

                            // Form Section
                            // Email Input
                            buildTextInput(
                              hint: 'Username',
                              controller: context.read<LoginCubit>().usernameController,
                            ),
                            const SizedBox(height: 24),

                            // Password Input
                            buildTextInput(
                              hint: 'Password',
                              obscureText: true,
                              controller: context.read<LoginCubit>().passwordController,
                            ),
                            const SizedBox(height: 24),

                            // Options
                            Row(
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                              children: [
                                // Remember Me
                                GestureDetector(
                                  onTap: () {
                                    // Toggle remember me
                                  },
                                  child: Row(
                                    children: [
                                      Container(
                                        width: 16,
                                        height: 16,
                                        decoration: BoxDecoration(
                                          color: const Color(0xFF334155),
                                          border: Border.all(
                                              color: const Color(0xFF475569)),
                                          borderRadius: BorderRadius.circular(4),
                                        ),
                                        // child: const Icon(Icons.check, size: 10, color: Colors.white), // If checked
                                      ),
                                      const SizedBox(width: 8),
                                      const Text(
                                        'Remember Me',
                                        style: TextStyle(
                                          fontWeight: FontWeight.w400,
                                          fontSize: 14,
                                          color: Color(0xFFCBD5E1),
                                        ),
                                      ),
                                    ],
                                  ),
                                ),
                                // Forgot Password
                              ],
                            ),
                            const SizedBox(height: 24),

                            // Sign In Button
                            SizedBox(
                              width: double.infinity,
                              height: 56,
                              child: ElevatedButton(
                                onPressed: () {
                                 context.read<LoginCubit>().login();
                                      
                                },
                                style: ElevatedButton.styleFrom(
                                  backgroundColor: const Color(0xFF2563EB),
                                  shape: RoundedRectangleBorder(
                                    borderRadius: BorderRadius.circular(8),
                                  ),
                                  elevation: 0,
                                ),
                                child: const Text(
                                  'Sign In',
                                  style: TextStyle(
                                    fontWeight: FontWeight.w700,
                                    fontSize: 16,
                                    color: Colors.white,
                                  ),
                                ),
                              ),
                            ),

                            const SizedBox(height: 32),

                            // Separator
                            Row(
                              children: [
                                const Expanded(
                                  child: Divider(
                                    color: Color(0xFF334155),
                                    thickness: 1,
                                  ),
                                ),
                                Padding(
                                  padding: const EdgeInsets.symmetric(horizontal: 16),
                                  child: const Text(
                                    'or',
                                    style: TextStyle(
                                      fontWeight: FontWeight.w400,
                                      fontSize: 14,
                                      color: Color(0xFF64748B),
                                    ),
                                  ),
                                ),
                                const Expanded(
                                  child: Divider(
                                    color: Color(0xFF334155),
                                    thickness: 1,
                                  ),
                                ),
                              ],
                            ),

                            const SizedBox(height: 20),

                            // Create Account
                            Row(
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: [
                                const Text(
                                  'New user? ',
                                  style: TextStyle(
                                    fontWeight: FontWeight.w400,
                                    fontSize: 14,
                                    color: Color(0xFF94A3B8),
                                  ),
                                ),
                                GestureDetector(
                                  onTap:()=> context.push('/register'),
                                  child: const Text(
                                    'Create Account',
                                    style: TextStyle(
                                      fontWeight: FontWeight.w500,
                                      fontSize: 14,
                                      color: Color(0xFF60A5FA),
                                    ),
                                  ),
                                ),
                              ],
                            ),

                            const SizedBox(height: 40),

                            // Footer
                            const Text(
                              '© 2024 AffiliateApp. All rights reserved.',
                              textAlign: TextAlign.center,
                              style: TextStyle(
                                fontWeight: FontWeight.w400,
                                fontSize: 12,
                                color: Color(0xFF64748B),
                              ),
                            ),
                          ],
                        ),
                      ),
                    ],
                  ),
                ),
              ),
            );
          },
        ),
      ),
    );
  }
}
