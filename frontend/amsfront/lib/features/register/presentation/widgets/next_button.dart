import 'package:amsfront/app/switcher.dart';
import 'package:amsfront/features/register/data/model/user_data.dart';

import 'package:flutter/material.dart';

  Widget buildNextButton(BuildContext context, String? selectedRole,UserData userData
   ,{required GlobalKey<FormState> formKey}) 
{
  
  
  
    return SizedBox(
      height: 56,
      child: Container(
        decoration: BoxDecoration(
          gradient: const LinearGradient(
            colors: [Color(0xFF2563EB), Color(0xFF1D4ED8)],
            begin: Alignment.topCenter,
            end: Alignment.bottomCenter,
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
          
          onPressed: () {
             if (formKey.currentState!.validate()) {
                           // Form is valid, proceed to the next step or submit.
                        // You can access all data from `userData` and `selectedRole`.
                        
                         switcher.roleRouting(context, selectedRole,userData);
                      }
        // switcher.roleRouting(context, selectedRole,userData);
       
          },
          style: ElevatedButton.styleFrom(
            backgroundColor: Colors.transparent,
            shadowColor: Colors.transparent,
            shape: RoundedRectangleBorder(borderRadius: BorderRadius.circular(12)),
          ),
          child: const Text(
            'Next',
            style: TextStyle(fontWeight: FontWeight.w700, fontSize: 16, color: Colors.white),
          ),
        ),
      ),
    );
  
  }