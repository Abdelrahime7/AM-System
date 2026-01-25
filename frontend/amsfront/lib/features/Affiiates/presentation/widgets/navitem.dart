

import 'package:flutter/material.dart';

class NavItem extends StatelessWidget {
  final String title;
  final IconData icon;
  final bool isActive;

  const NavItem({
    required this.title,
    required this.icon,
    this.isActive = false,
  });


  @override
  Widget build(BuildContext context) {
    return Expanded(
      child: InkWell(
        onTap: () {},
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(
              icon,
              size: 24,
              color: isActive ? Colors.white : Colors.white.withOpacity(0.3),
            ),
            const SizedBox(height: 4),
            Text(
              title,
              style: TextStyle(
                fontSize: 12,
                fontWeight: FontWeight.w500,
                color: isActive ? Colors.white : Colors.white.withOpacity(0.3),
                letterSpacing: 0.18,
              ),
            ),
          ],
        ),
      ),
    );
  }
}
