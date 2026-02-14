



import 'package:amsfront/app/di/injector/injectors.dart';
import 'package:amsfront/features/register/data/model/register.dart';
import 'package:amsfront/features/register/data/model/user_data.dart';
import 'package:amsfront/features/register/presentation/cubit/register_state.dart';
import 'package:dio/dio.dart';
import 'package:flutter/material.dart';

import 'package:flutter_bloc/flutter_bloc.dart';


class RegisterCubit  extends Cubit<RegisterState> {
  final Roleregister roleregister; 
  final UserData userData; 
 

  RegisterCubit(this.roleregister,this.userData) : super(RegisterInitial());

 

Future<void> register() async {
    emit(RegisterLoading());  
 try{
 await roleregister.register(userData);
emit(RegisterSuccess('your account has been created successfully ,wait for admin approval' ));


}on DioException catch (e) { // ✅ correct for Dio v5
  print('Dio error: $e');
  if (e.response != null) print('Response data: ${e.response?.data}');
  String errorMessage = 'An error occurred';

  if (e.response != null) {
    final data = e.response!.data;

    if (data is Map && data.containsKey('message')) {
      errorMessage = data['message'];
    } else if (data is String) {
      errorMessage = data;
    } else {
      errorMessage = data.toString();
    }
  } else {
    errorMessage = 'Network error. Please try again.';
  }

  emit(RegisterFailure(errorMessage));
 }
 catch(e){
  print('Error: $e');
  emit(RegisterFailure("register failure ${e.toString()}"));
 }
 
}

}

class RegisterBlocWrapper extends StatelessWidget {
  final Widget Function(BuildContext) builder;
  final RegisterCubit? cubit;

  const RegisterBlocWrapper({super.key, required this.builder, this.cubit});

  @override
  Widget build(BuildContext context) {
    return BlocProvider(
      create: (context) => cubit ?? getIt<RegisterCubit>(),
      child: BlocConsumer<RegisterCubit, RegisterState>(
        listener: (context, state) {
          if (state is RegisterSuccess) {
            ScaffoldMessenger.of(context).showSnackBar(
              SnackBar(content: Text(state.message)),
            );
          } else if (state is RegisterFailure) {
            ScaffoldMessenger.of(context).showSnackBar(
              SnackBar(
                content: Text(state.error),
                backgroundColor: Colors.red,
              ),
            );
          }
        },
        builder: (context, state) {
          if (state is RegisterLoading) {
            return const Scaffold(
              backgroundColor: Color(0xFF111722),
              body: Center(child: CircularProgressIndicator()),
            );
          }
          // This passes the context *under* the BlocProvider to your screen
          return builder(context);
        },
      ),
    );
  }
}