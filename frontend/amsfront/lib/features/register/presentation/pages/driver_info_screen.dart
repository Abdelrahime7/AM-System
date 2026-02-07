import 'package:amsfront/features/register/presentation/cubit/register_cubit.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart'; 
import 'package:amsfront/features/register/presentation/widgets/welcome_section.dart';
import 'package:flutter_bloc/flutter_bloc.dart';


  

class Driver_infoScreen extends StatefulWidget {
  const Driver_infoScreen ({super.key}); 

  @override
  State<Driver_infoScreen> createState() => _Driver_infoScreenState();
}

class _Driver_infoScreenState extends State<Driver_infoScreen> {
  String? statusSelectedValue;
  String? availabilitySelectedValue;

  final List<String> statusType = const [
    'Local',
  ];
  final List<String> availabilityTypes = const [
    'available',
  ];

  void _onStatusChanged(String? value) {
    if (value == null) return;
    setState(() {
      statusSelectedValue = value;
    });
    // Update Cubit: true if Local, false if External
   // context.read<RegisterCubit>().islocal = (value == 'Local');
  }

  void _onAvailabilityChanged(String? value) {
    if (value == null) return;
    setState(() {
      availabilitySelectedValue = value;
    });
    // Update Cubit: true if available
//    context.read<RegisterCubit>().isAvailable = (value == 'available');
  }

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
                buildWelcomeSection("Driver Info", 'Enter your details below'),
                const SizedBox(height: 32),
                ...statusType.map((type) => RadioListTile<String>(
                      contentPadding: EdgeInsets.zero,
                      activeColor: const Color(0xFF2563EB),
                      title: Text(
                        type,
                        style: const TextStyle(color: Colors.white, fontSize: 16),
                      ),
                      value: type,
                      groupValue: statusSelectedValue,
                      onChanged: _onStatusChanged,
                    )),
                ...availabilityTypes.map((type) => RadioListTile<String>(
                      contentPadding: EdgeInsets.zero,
                      activeColor: const Color(0xFF2563EB),
                      title: Text(
                        type,
                        style: const TextStyle(color: Colors.white, fontSize: 16),
                      ),
                      value: type,
                      groupValue: availabilitySelectedValue,
                      onChanged: _onAvailabilityChanged,
                    )),


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
                      
                      onPressed: () =>

                      context.read<RegisterCubit>().register(),
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
  
