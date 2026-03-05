import 'package:amsfront/features/Assisstant/presentation/widgets/actionbutton.dart';
import 'package:flutter/material.dart';


class OrderCard extends StatelessWidget {
  final Map<String, String> order;

  const OrderCard({required this.order});

  @override
  Widget build(BuildContext context) {
    return Container(
      height: 130,
      margin: const EdgeInsets.only(bottom: 16),
      padding: const EdgeInsets.symmetric(horizontal: 17, vertical: 16),
      decoration: BoxDecoration(
        color: const Color(0xFF182134),
        borderRadius: BorderRadius.circular(8),
      ),
      child: Row(
        children: [
          // Order Info
          Expanded(
            child: Column(
              crossAxisAlignment: CrossAxisAlignment.start,
              mainAxisAlignment: MainAxisAlignment.spaceBetween,
              children: [
                Text(
                  'Order ID: #${order['id']}',
                  style: const TextStyle(
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w500,
                    fontSize: 16,
                    color: Colors.white,
                  ),
                ),
                Text(
                  'Customer: ${order['customer']}',
                  style: const TextStyle(
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w400,
                    fontSize: 14,
                    color: Color(0xFF94A6C7),
                  ),
                ),
                Text(
                  order['amount']!,
                  style: const TextStyle(
                    fontFamily: 'Inter',
                    fontWeight: FontWeight.w400,
                    fontSize: 16,
                    color: Colors.white,
                  ),
                ),
              ],
            ),
          ),
          
          const SizedBox(width: 10),

          // Action Buttons
          Column(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              ActionButton(
                text: 'Approve',
                textColor: const Color(0xFF22C55E),
                backgroundColor: const Color.fromRGBO(34, 197, 94, 0.2),
                onTap: () => debugPrint('Approved order: ${order['id']}'),
              ),
              ActionButton(
                text: 'Reject',
                textColor: const Color(0xFFEF4444),
                backgroundColor: const Color.fromRGBO(239, 68, 68, 0.2),
                onTap: () => debugPrint('Rejected order: ${order['id']}'),
              ),
            ],
          ),
        ],
      ),
    );
  }
}