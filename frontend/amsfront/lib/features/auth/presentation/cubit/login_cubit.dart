import 'package:amsfront/features/auth/presentation/cubit/login_state.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter/material.dart';

class LoginCubit extends Cubit<LoginState> {
  LoginCubit() : super(LoginInitial());

  final TextEditingController usernameController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();

  void login() async {
    emit(LoginLoading());
    // Simulate a network request
    await Future.delayed(const Duration(seconds: 2));

    if (usernameController.text == "abdou" &&
        passwordController.text == "12345") {
      emit(LoginSuccess('Login successful!'));
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
