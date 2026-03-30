import 'package:flutter/material.dart';

class ActionCard extends StatelessWidget {
  final Map<String, dynamic> data;
  final VoidCallback? onPressed;

  const ActionCard({super.key, required this.data, this.onPressed});

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onPressed,
      borderRadius: BorderRadius.circular(8),
      child: Container(
        padding: const EdgeInsets.all(16),
        constraints: const BoxConstraints(minHeight: 90),
        decoration: BoxDecoration(
          color: const Color(0xFF1A2433),
          border: Border.all(color: const Color(0xFF334566)),
          borderRadius: BorderRadius.circular(8),
        ),
        child: Row(
          children: [
            Icon(data['icon'], color: Colors.white, size: 24),
            const SizedBox(width: 12),
            Expanded(
              child: Text(
                data['title'],
                style: const TextStyle(
                  fontSize: 16,
                  fontWeight: FontWeight.w700,
                  color: Colors.white,
                ),
              ),
            ),
          ],
        ),
      ),
    );
  }
}