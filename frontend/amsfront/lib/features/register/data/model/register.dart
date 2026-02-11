

import 'package:amsfront/features/register/data/model/register_Data.dart';
import 'package:amsfront/features/register/data/model/user_data.dart';
import 'package:amsfront/features/register/data/repositories/register_repository.dart';
import 'package:flutter/material.dart';

 class Roleregister{
 Future<void> register(UserData userData) async {
   // Default implementation:
     UnimplementedError('register() must be implemented in subclass');

      }
       
}

class AffiliateRegister implements Roleregister{

  RegisterRepository registerRepository ;  
  AffiliateRegister(this.registerRepository); 

final TextEditingController referalcodeController =TextEditingController();
final TextEditingController commisionrateController=TextEditingController();

  @override
  Future<void> register(UserData userData) async {
     final object = AffiliateRegisterData( 
    //commisionrate: commisionrateController.text, 
    referalcode: referalcodeController.text,
     userData: userData, ); 
     await registerRepository.register(object); 
     }
 

  void dispose()
  {
    referalcodeController.dispose();
    commisionrateController.dispose();
  }

}
class AssisstantRegister implements Roleregister
{
RegisterRepository registerRepository ;  
final TextEditingController assignedBy= TextEditingController();

AssisstantRegister(this.registerRepository);
 

  @override
  Future<void> register(UserData userData) async {
    final  object =   AssistantRegisterData(
      userData: userData, 
      assignedBy:1,
      );
      await registerRepository.register(object);
  }
  

}