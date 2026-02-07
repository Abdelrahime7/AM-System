


import 'package:amsfront/features/register/data/model/register.dart';
import 'package:amsfront/features/register/data/model/user_data.dart';
import 'package:amsfront/features/register/presentation/cubit/register_state.dart';

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

 }
 catch(e){
  emit(RegisterFailure("register failure ${e.toString()}"));
 }
 
}

}