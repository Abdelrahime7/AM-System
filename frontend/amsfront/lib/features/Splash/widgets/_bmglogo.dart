
import 'package:flutter/material.dart';

class BmgLogo extends StatelessWidget {
  const BmgLogo({super.key});

  @override
  Widget build(BuildContext context) {
    return Column(
      mainAxisSize: MainAxisSize.min,
      children: [
        const Text(
          'BMG',
          style: TextStyle(
            fontSize: 72,
            fontWeight: FontWeight.w900,
            color: Colors.white,
            letterSpacing: 8,
          ),
        ),
        const SizedBox(height: 8),
        Container(
          width: 280,
          height: 4,
          color: Colors.white,
        ),
        const SizedBox(height: 12),
        Row(
          mainAxisSize: MainAxisSize.min,
          crossAxisAlignment: CrossAxisAlignment.center,
          children: [
            const Text(
              'corporation',
              style: TextStyle(
                fontSize: 18,
                fontWeight: FontWeight.w300,
                color: Color(0xFF9CA3AF),
                letterSpacing: 3,
              ),
            ),
            const SizedBox(width: 12),
            Container(
              width: 20,
              height: 20,
              decoration: BoxDecoration(
                shape: BoxShape.circle,
                border: Border.all(
                  color: const Color(0xFF9CA3AF),
                  width: 1.5,
                ),
              ),
              child: const Center(
                child: Text(
                  '©',
                  style: TextStyle(
                    fontSize: 12,
                    fontWeight: FontWeight.bold,
                    color: Color(0xFF9CA3AF),
                  ),
                ),
              ),
            ),
          ],
        ),
      ],
    );
  }
}