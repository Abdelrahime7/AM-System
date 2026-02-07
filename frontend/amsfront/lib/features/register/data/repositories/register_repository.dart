
import 'package:amsfront/features/register/data/model/register_Data.dart';
import 'package:amsfront/features/register/data/service/register_service.dart';

class RegisterRepository {
RegisterService registerService;
RegisterRepository(this.registerService);

 Future  register (RegisterPayload registerData)
 {
   try
   {
     return registerService.register(registerData);
   }
   catch(e)
   {
     throw Exception("register failed: $e");
   }
 }
  
}