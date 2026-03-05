
import 'package:amsfront/features/register/data/model/user_data.dart';

import 'package:amsfront/features/register/presentation/widgets/form.dart';
import 'package:amsfront/features/register/presentation/widgets/header.dart';
import 'package:amsfront/features/register/presentation/widgets/next_button.dart';
import 'package:amsfront/features/register/presentation/widgets/welcome_section.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:go_router/go_router.dart';

class RegisterScreen extends StatefulWidget {
  const RegisterScreen({super.key});

  @override
  State<RegisterScreen> createState() => _RegisterScreenState();
}

class _RegisterScreenState extends State<RegisterScreen> {
final _formKey = GlobalKey<FormState>();
final TextEditingController emailController =TextEditingController();
final TextEditingController fullnameController=TextEditingController() ;
final TextEditingController phoneController =TextEditingController();
final TextEditingController usernameController =TextEditingController();
final TextEditingController passwordController =TextEditingController();
final TextEditingController confirmPasswordController =TextEditingController();


  UserData userData = UserData();
  String? selectedRole;
  bool showRoleDropdown = false;
  final List<String> roles = const [
    'Admin',
    'Assisstant' ,
    'Affiliate',
    'Driver'
  ];

  @override
  Widget build(BuildContext context) {
    
    return AnnotatedRegion<SystemUiOverlayStyle>(
      value: const SystemUiOverlayStyle(
        statusBarColor: Colors.transparent,
        statusBarIconBrightness: Brightness.light,
      ),
    

        
       child: Scaffold(
        backgroundColor: const Color(0xFF111722),
        body: SafeArea(
          child: SingleChildScrollView(
            padding: const EdgeInsets.only(bottom: 40),
            child: Padding(
              padding: const EdgeInsets.symmetric(horizontal: 24.0),
              child: Column(
                crossAxisAlignment: CrossAxisAlignment.stretch,
                children: [
                  // Header
                  Padding(
                    padding: const EdgeInsets.only(top: 24.0),
                    child: buildHeader(),
                  ),
                  const SizedBox(height: 32),

                  // Welcome Section
                  buildWelcomeSection('Create Account','join our team'),
                  const SizedBox(height: 32),

                  // Form Fields
                  Form(
                    key: _formKey,
                    child: Column(
                      children: [
                        buildForm(
                            userData,
                            fullnameController,
                            emailController,
                            phoneController,
                            usernameController,
                            passwordController,
                            confirmPasswordController),
                        const SizedBox(height: 16),
                        buildRoleDropdown(),
                      ],
                    ),
                  ),
                  const SizedBox(height: 16),
                buildNextButton(context,selectedRole,userData, formKey:_formKey),

                  // Separator and Sign In Link
                  Padding(
                    padding: const EdgeInsets.symmetric(vertical: 24.0),
                    child: Column(
                      children: [
                        _buildSeparator(),
                        const SizedBox(height: 20),
                        _buildSignInLink(context),
                      ],
                    ),
                  ),

                  // Footer
                  const SizedBox(height: 32),
                  _buildFooter(),
                ],
              ),
            ),
          ),
        ),
       )
      
      
    );

      
    
  }

 
 
   Widget buildRoleDropdown() {
    return FormField<String>(
      initialValue: selectedRole,
      validator: (value) {
        
        if (value == null || value.isEmpty) {
          return 'Please select a role.';
        }
        return null;
      },
      builder: (FormFieldState<String> state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            GestureDetector(
              onTap: () => setState(() {
                showRoleDropdown = !showRoleDropdown;
              }),
              child: Container(
                height: 56,
                padding: const EdgeInsets.symmetric(horizontal: 24),
                decoration: BoxDecoration(
                  color: const Color(0xFF1F2937),
                  border: Border.all(
                      color: state.hasError ? Colors.red : const Color(0xFF374151)),
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      selectedRole ?? 'Select your role',
                      style: TextStyle(
                        fontSize: 16,
                        color: selectedRole == null
                            ? const Color(0xFF9CA3AF)
                            : Colors.white,
                      ),
                    ),
                    const Icon(Icons.arrow_drop_down, color: Color(0xFF6B7280)),
                  ],
                ),
              ),
            ),
            if (state.hasError)
              Padding(
                padding: const EdgeInsets.only(top: 8.0, left: 12.0),
                child: Text(
                  state.errorText!,
                  style: const TextStyle(color: Colors.red, fontSize: 12),
                ),
              ),
            if (showRoleDropdown)
              Container(
                margin: const EdgeInsets.only(top: 4),
                decoration: BoxDecoration(
                  color: const Color(0xFF1F2937),
                  border: Border.all(color: const Color(0xFF374151)),
                  borderRadius: BorderRadius.circular(12),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.2),
                      blurRadius: 8,
                      offset: const Offset(0, 4),
                    ),
                  ],
                ),
                child: Column(
                  children: roles.map((role) => InkWell(
                        onTap: () {
                          setState(() {
                            selectedRole = role;
                            showRoleDropdown = false;
                          });
                          state.didChange(role);
                        },
                        child: Container(
                          width: double.infinity,
                          padding: const EdgeInsets.symmetric(
                              horizontal: 24, vertical: 16),
                          decoration: BoxDecoration(
                            border: role != roles.last
                                ? const Border(
                                    bottom: BorderSide(color: Color(0xFF374151)))
                                : null,
                          ),
                          child: Text(role,
                              style: const TextStyle(
                                  fontSize: 16, color: Colors.white)),
                        ),
                      )).toList(),
                ),
              ),
          ],
        );
      },
    );
  }


  Widget _buildSeparator() {
    return Row(children: const [
      Expanded(child: Divider(color: Color(0xFF334155), thickness: 1)),
      Padding(
        padding: EdgeInsets.symmetric(horizontal: 16),
        child: Text('or',
            style: TextStyle(fontSize: 14, color: Color(0xFF64748B))),
      ),
      Expanded(child: Divider(color: Color(0xFF334155), thickness: 1)),
    ]);
  }

  Widget _buildSignInLink(BuildContext context) {
    return Row(mainAxisAlignment: MainAxisAlignment.center, children: [
      const Text('Already have an account? ',
          style: TextStyle(fontSize: 14, color: Color(0xFF94A3B8))),
      GestureDetector(
        onTap:()=> context.push('/login'),
        child: const Text(
          'Sign In',
          style: TextStyle(
              fontWeight: FontWeight.w500, fontSize: 14, color: Color(0xFF60A5FA)),
        ),
      ),
    ]);
  }

  Widget _buildFooter() {
    return const Text(
      '© 2024 AffiliateApp. All rights reserved.',
      textAlign: TextAlign.center,
      style: TextStyle(fontSize: 12, color: Color(0xFF64748B)),
    );
  }
}