import 'package:flutter/material.dart';

class OrderCard extends StatelessWidget {
  final Map<String, dynamic> order;

  const OrderCard({required this.order});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 92,
      margin: const EdgeInsets.only(bottom: 12),
      decoration: BoxDecoration(
        color: const Color(0xFF182134),
        borderRadius: BorderRadius.circular(8),
      ),
      child: Material(
        color: Colors.transparent,
        child: InkWell(
          onTap: () {},
          borderRadius: BorderRadius.circular(8),
          child: Padding(
            padding: const EdgeInsets.all(12),
            child: Row(
              children: [
                // Image Placeholder
                Container(
                  width: 64,
                  height: 64,
                  margin: const EdgeInsets.only(right: 17),
                  decoration: BoxDecoration(
                    color: const Color(0xFF2A3441),
                    borderRadius: BorderRadius.circular(8),
                  ),
                ),
                
                // Order Info
                Expanded(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                      Text(
                        'Customer: ${order['customerName']}',
                        style: const TextStyle(
                          fontFamily: 'Inter',
                          fontWeight: FontWeight.w600,
                          fontSize: 16,
                          color: Colors.white,
                        ),
                      ),
                      const SizedBox(height: 4),
                      Text(
                        'ID: ${order['customerId']} | Order: ${order['orderNumber']}',
                        style: const TextStyle(
                          fontFamily: 'Inter',
                          fontWeight: FontWeight.w400,
                          fontSize: 14,
                          color: Color(0xFF90A2CB),
                        ),
                      ),
                    ],
                  ),
                ),

                // Chevron Icon
                const Padding(
                  padding: EdgeInsets.only(left: 12),
                  child: Icon(
                    Icons.chevron_right,
                    color: Color(0xFF9CA3AF),
                    size: 24,
                  ),
                ),
              ],
            ),
          ),
        ),
      ),
    );
  }
}
