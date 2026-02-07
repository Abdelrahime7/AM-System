 import 'package:flutter/material.dart';

Widget buildTextInput({
  required String hint,
  required TextEditingController controller,
  bool obscureText = false,
  TextInputType keyboardType = TextInputType.text,
  Function(String value)? onChanged,
  String? Function(String?)? validator,
  String? prefixText,
}) {
  return TextFormField(
    onChanged: onChanged,
    controller: controller,
    obscureText: obscureText,
    keyboardType: keyboardType,
    style: const TextStyle(fontSize: 16, color: Colors.white),
    validator: validator,
    decoration: InputDecoration(
      prefixText: prefixText,
      hintText: hint,
      hintStyle: const TextStyle(fontSize: 16, color: Color(0xFF9CA3AF)),
      filled: true,
      fillColor: const Color(0xFF1A2333),
      contentPadding:
          const EdgeInsets.symmetric(horizontal: 24, vertical: 18.5),
      enabledBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(8),
        borderSide: const BorderSide(color: Color(0xFF334155)),
      ),
      focusedBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(8),
        borderSide: const BorderSide(color: Color(0xFF2563EB)),
      ),
      errorBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(8),
        borderSide: const BorderSide(color: Colors.red),
      ),
      focusedErrorBorder: OutlineInputBorder(
        borderRadius: BorderRadius.circular(8),
        borderSide: const BorderSide(color: Colors.red),
      ),
    ),
  );
}
