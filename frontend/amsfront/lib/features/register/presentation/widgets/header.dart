import 'package:flutter/material.dart';


 Widget buildHeader() {
    return Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        const SizedBox(width: 107, height: 28), // Logo placeholder
        InkWell(
          onTap: () {},
          borderRadius: BorderRadius.circular(9999),
          child: Container(
            width: 83,
            height: 38,
            decoration: BoxDecoration(
              border: Border.all(color: const Color(0xFF475569)),
              borderRadius: BorderRadius.circular(9999),
            ),
            alignment: Alignment.center,
            child: const Text(
              'EN / AR',
              style: TextStyle(
                fontWeight: FontWeight.w500,
                fontSize: 14,
                color: Colors.white,
              ),
            ),
          ),
        ),
      ],
    );
  }
