import 'package:amsfront/app/di/injector/injectors.dart';
import 'package:amsfront/app/enums/roles.dart';
import 'package:amsfront/features/register/data/model/register.dart';
import 'package:amsfront/features/register/data/model/user_data.dart';
import 'package:amsfront/features/register/presentation/cubit/register_cubit.dart';
import 'package:amsfront/features/register/presentation/widgets/text_input.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart'; 
import 'package:amsfront/features/register/presentation/widgets/welcome_section.dart';
import 'package:flutter_bloc/flutter_bloc.dart';

  


class Assisstant_infoScreen extends StatelessWidget {

  final String selectedRole;
  final UserData userData ;

 Assisstant_infoScreen({super.key , required this.selectedRole, required this.userData});
 
 final AssisstantRegister affiliateRegister = getIt<AssisstantRegister>();
          final _formKey = GlobalKey<FormState>();


  
  @override
  Widget build(BuildContext context) {
      factor(selectedRole , userData);

    return Form(key:_formKey ,
    child: 
    AnnotatedRegion<SystemUiOverlayStyle>(
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
                  controller: affiliateRegister.assignedBy,
                  validator: (value) {
                             if (value == null || value.isEmpty ) {
                             return ' is required';
                              }
                               return null;
                             }
                  
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
              
                      onPressed: () {
                                if (_formKey.currentState!.validate()) {
                                  // Form is valid, proceed with registration
                                  userData.role = roles.Assistant;
                                  context.read<RegisterCubit>().register()
                              ;
                                }
                      },
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
              );
              

            } ,
    ),
    )
    );
        
    
    


    
  }






}
