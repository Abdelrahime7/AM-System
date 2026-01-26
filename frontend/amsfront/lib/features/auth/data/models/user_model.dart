import 'package:amsfront/app/enums/roles.dart';

class UserModel 
{
   final String id; 
   final String email;
   final String token;
   final roles role;
   
   
     UserModel({required this.id, required this.email, required this.token, required this.role });

      factory UserModel.fromJson(Map<String, dynamic> json)
       { 
        // Helper to safely parse the role string from the API into a `roles` enum.
        // It handles potential nulls and unknown role strings gracefully.
        roles parseRole(String? roleString) {
          if (roleString == null) {
            return roles.Assistant; // Default role if null
          }
          return roles.values.firstWhere(
            (e) => e.name.toLowerCase() == roleString.toLowerCase(),
            orElse: () => roles.Assistant, // Fallback for unknown roles
          );
        }

        return UserModel( 
          id: json['id'],
           email: json['email'],
            token: json['token'], 
            role: parseRole(json['role'])
        );
      }
          
    }  