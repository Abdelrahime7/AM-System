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
  int role = 0;

  Future<void> login() async {
    emit(LoginLoading());
    try {
      String username = usernameController.text;
      String password = passwordController.text;
      final user = await repository.login(username, password);

      emit(LoginSuccess("login successful"));
      role = user.role;
    } on DioException catch (e) {
     
      emit(LoginFailure(e.response?.data['message'] ?? 'A network error occurred. Please try again.'));
    } catch (e) {
      emit(LoginFailure('An unexpected error occurred. Please try again.'));
    }
  }

  @override
  Future<void> close() {
    usernameController.dispose();
    passwordController.dispose();
    return super.close();
  }
}
