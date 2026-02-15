import 'package:amsfront/app/enums/roles.dart';
import 'package:amsfront/app/enums/userStatus.dart';
import 'package:amsfront/features/auth/presentation/cubit/login_state.dart';
import 'package:dio/dio.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:flutter/material.dart';
import 'package:amsfront/features/auth/data/repositories/auth_repository.dart';

class LoginCubit extends Cubit<LoginState> {
  final AuthRepository repository;
  LoginCubit(this.repository) : super(LoginInitial());

  final TextEditingController usernameController = TextEditingController();
  final TextEditingController passwordController = TextEditingController();
  late roles role ;
  late UserStatus status ;


  Future<void> login() async {
    emit(LoginLoading());
    try {
  final user = await repository.login(
    usernameController.text,
    passwordController.text,
  );
  status = user.status;
  role = user.role;
  emit(LoginSuccess("Login successful"));
} on DioException catch (e) { // ✅ correct for Dio v5
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

  emit(LoginFailure(errorMessage));
  } catch (e) {
     

  emit(LoginFailure('Unexpected error: ${e.toString()}'));


}

  }

  @override
  Future<void> close() {
    usernameController.dispose();
    passwordController.dispose();
    return super.close();
  }
}
