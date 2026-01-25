import 'dart:math';

import 'package:amsfront/app/enums/roles.dart';
import 'package:amsfront/features/auth/presentation/cubit/login_state.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter/material.dart';

class LoginCubit extends Cubit<LoginState> {
  final random = Random(); // random int from 0 to 4 Roles role = Roles.values[r];
  LoginCubit() : super(LoginInitial());


  final TextEditingController usernameController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();
  roles role =roles.Assistant;
  


  void login() async {
    emit(LoginLoading());
    // Simulate a network request
    await Future.delayed(const Duration(seconds: 2));



    if (usernameController.text == "Username1" &&
        passwordController.text == "12345") {
      emit(LoginSuccess('Login successful'));
 
    } else {
      emit(LoginFailure('Invalid credentials.'));
    }
  }

  @override
  Future<void> close() {
    usernameController.dispose();
    passwordController.dispose();
    return super.close();
  }
}
