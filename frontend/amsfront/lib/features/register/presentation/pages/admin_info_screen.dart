import 'package:amsfront/app/di/injector/injectors.dart';
import 'package:amsfront/app/enums/access_levels.dart';
import 'package:amsfront/app/enums/roles.dart';
import 'package:amsfront/features/register/data/model/register.dart';
import 'package:amsfront/features/register/data/model/user_data.dart';
import 'package:amsfront/features/register/presentation/cubit/register_cubit.dart';
import 'package:amsfront/features/register/presentation/widgets/welcome_section.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

class Admin_infoScreen extends StatefulWidget {
  final String selectedRole;
  final UserData userData;

  const Admin_infoScreen({
    super.key,
    required this.selectedRole,
    required this.userData,
  });

  @override
  State<Admin_infoScreen> createState() => _Admin_infoScreenState();
}

class _Admin_infoScreenState extends State<Admin_infoScreen> {

  final AdminRegister adminRegister = getIt<AdminRegister>();
  final _formKey = GlobalKey<FormState>();

  accesslevels? selectedLevel;
  bool showDropdown = false;

  @override
  Widget build(BuildContext context) {
    
    factor(widget.selectedRole, widget.userData);

    return Form(
      key: _formKey,
      child: AnnotatedRegion<SystemUiOverlayStyle>(
        value: const SystemUiOverlayStyle(
          statusBarColor: Colors.transparent,
          statusBarIconBrightness: Brightness.light,
        ),
        child: RegisterBlocWrapper(
         
          builder: (context) {
            return Scaffold(
              backgroundColor: const Color(0xFF111722),
              extendBodyBehindAppBar: true,
              appBar: AppBar(
                backgroundColor: Colors.transparent,
                elevation: 0,
                leading: IconButton(
                  icon: const Icon(Icons.arrow_back_ios_new_rounded,
                      color: Colors.white),
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
                      buildWelcomeSection("Admin Info", 'Fill your infos'),
                      const SizedBox(height: 32),
                      _buildAccessLevelDropdown(),
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
                            onPressed: () {
                              if (_formKey.currentState!.validate()) {
                               
                                widget.userData.role = roles.Admin;
                                context.read<RegisterCubit>().register();
                              }
                            },
                            style: ElevatedButton.styleFrom(
                              backgroundColor: Colors.transparent,
                              shadowColor: Colors.transparent,
                              shape: RoundedRectangleBorder(
                                  borderRadius: BorderRadius.circular(12)),
                            ),
                            child: const Text(
                              'Register',
                              style: TextStyle(
                                  fontWeight: FontWeight.w700,
                                  fontSize: 16,
                                  color: Colors.white),
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

  Widget _buildAccessLevelDropdown() {
    return FormField<accesslevels>(
      validator: (value) {
        if (selectedLevel == null) {
          return 'Please select an access level.';
        }
        return null;
      },
      builder: (FormFieldState<accesslevels> state) {
        return Column(
          crossAxisAlignment: CrossAxisAlignment.stretch,
          children: [
            GestureDetector(
              onTap: () => setState(() {
                showDropdown = !showDropdown;
              }),
              child: Container(
                height: 56,
                padding: const EdgeInsets.symmetric(horizontal: 24),
                decoration: BoxDecoration(
                  color: const Color(0xFF1F2937),
                  border: Border.all(
                      color: state.hasError
                          ? Colors.red
                          : const Color(0xFF374151)),
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: [
                    Text(
                      selectedLevel?.name ?? 'Select Access Level',
                      style: TextStyle(
                        fontSize: 16,
                        color: selectedLevel == null
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
            if (showDropdown)
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
                  children: accesslevels.values.map((level) => InkWell(
                        onTap: () {
                          setState(() {
                            selectedLevel=level;
                            adminRegister.accessLevel = level;
                            showDropdown = false;
                          });
                          state.didChange(level);
                        },
                        child: Container(
                          width: double.infinity,
                          padding: const EdgeInsets.symmetric(
                              horizontal: 24, vertical: 16),
                          decoration: BoxDecoration(
                            border: level != accesslevels.values.last
                                ? const Border(
                                    bottom:
                                        BorderSide(color: Color(0xFF374151)))
                                : null,
                          ),
                          child: Text(level.name,
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
}