import 'package:amsfront/features/register/data/model/user_data.dart';
import 'package:amsfront/features/register/presentation/widgets/text_input.dart';
import 'package:flutter/material.dart';


 Widget buildForm( UserData userData ,
 TextEditingController fullnameController,
  TextEditingController emailController,
   TextEditingController phoneController,
    TextEditingController usernameController,
     TextEditingController passwordController,
      TextEditingController confirmPasswordController,
      )
 
  { 
    return Column(
      children: <Widget>[
         buildTextInput(
          hint: 'Full Name',
          controller: fullnameController,
          onChanged: (value) => userData.fullname = value,
          validator: (value) {
            if (value == null || value.isEmpty) {
              return 'Full Name is required';
            }
            return null;
          },
        ),
                const SizedBox(height: 16),

          buildTextInput(
          hint: 'Email',
          controller: emailController ,
          onChanged: (value) => userData.email = value,
          validator: (value) {
            if (value == null || value.isEmpty) {
              return 'Email is required';
            }
            final emailRegex = RegExp(r'^[^@]+@[^@]+\.[^@]+');
            if (!emailRegex.hasMatch(value)) {
              return 'Enter a valid email';
            }
            return null;
          },
        ),
                const SizedBox(height: 16),

          buildTextInput(
          hint: 'phone',
          controller: phoneController ,
          onChanged: (value) {
            if (!value.startsWith('+213'))
             { phoneController.text = '+213${value.replaceAll('+213', '')}';
              phoneController.selection = TextSelection.fromPosition(
                 TextPosition(offset: phoneController.text.length),
                  );
               }
             userData.phone = value;
          },
          validator: (value) {
            if (value == null || value.isEmpty) {
              return 'Phone number is required';
            }
            return null;
          },
        ),
        const SizedBox(height: 16),
        buildTextInput(
          hint: 'Username',
          controller: usernameController,
          onChanged: (value) => userData.username = value,
          validator: (value) {
            if (value == null || value.isEmpty) {
              return 'Username is required';
            }
            return null;
          },
        ),
        const SizedBox(height: 16),
        buildTextInput(
          hint: 'password',
          controller: passwordController,
          onChanged: (value) => userData.password = value,
          validator: (value) {
            if (value == null || value.length < 8) {
              return 'Password must be at least 8 characters';
            }
            return null;
          },
                    obscureText: true,

          
        ),
        const SizedBox(height: 16),  
      
        buildTextInput(
          hint: 'Confirm Password',
          controller: confirmPasswordController,
          onChanged: (value) => userData.confirmPassword = value,
          obscureText: true,
          validator: (value) {
            if (value != passwordController.text) {
              return 'Passwords do not match';
            }
            return null;
          },
        ),

        const SizedBox(height: 16),

      ],
    );
  }
