import 'package:flutter/material.dart';

 Widget buildWelcomeSection(String initialtext, String secandary) {
  String _initialText = initialtext;
  String _secondaryText = secandary;


    return Column(
      children: 
      [
        Text(_initialText
          ,
          textAlign: TextAlign.center,
          style: TextStyle(
            fontWeight: FontWeight.w700,
            fontSize: 30,
            color: Colors.white,
            height: 1.2,
          ),
        ),
        SizedBox(height: 8),
        Text(
           _secondaryText,
          textAlign: TextAlign.center,
          style: TextStyle(
            fontWeight: FontWeight.w400,
            fontSize: 16,
            color: Color(0xFF94A3B8),
            height: 1.5,
          ),
        ),
      ],
    );
  }