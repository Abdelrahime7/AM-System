import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:amsfront/features/Affiiates/presentation/widgets/navitem.dart';
import 'package:amsfront/features/Affiiates/presentation/widgets/ordercard.dart';


class AffiliateScreen extends StatelessWidget {
  const AffiliateScreen({super.key});

  @override
  Widget build(BuildContext context) {
    // Mock Data
    final List<Map<String, dynamic>> orders = [
      {
        'id': '12345',
        'products': 'Product A, Product B',
        'price': '\$125.00',
        'commission': '+\$12.50',
        'date': 'Oct 23, 2023',
        'status': 'Delivered',
        'statusColor': const Color(0xFF4ADE80),
        'statusBg': const Color.fromRGBO(34, 197, 94, 0.2),
      },
      {
        'id': '12346',
        'products': 'Product C',
        'price': '\$75.00',
        'commission': '+\$7.50',
        'date': 'Oct 22, 2023',
        'status': 'En Route',
        'statusColor': const Color(0xFF60A5FA),
        'statusBg': const Color.fromRGBO(59, 130, 246, 0.2),
      },
      {
        'id': '12347',
        'products': 'Product D, Product E, Product F',
        'price': '\$250.00',
        'commission': '+\$25.00',
        'date': 'Oct 21, 2023',
        'status': 'Pending',
        'statusColor': const Color(0xFFFACC15),
        'statusBg': const Color.fromRGBO(234, 179, 8, 0.2),
      },
    ];

    return AnnotatedRegion<SystemUiOverlayStyle>(
      value: const SystemUiOverlayStyle(
        statusBarColor: Color(0xFF111722),
        statusBarIconBrightness: Brightness.light,
      ),
      child: Scaffold(
      appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 44, 54, 83),
        elevation: 0,
        iconTheme: const IconThemeData(color: Colors.white),
      ),
      
        body: SafeArea(

          child: Column(
            children: [
             
                
              Container(
                height: 72,
                padding: const EdgeInsets.symmetric(horizontal: 16),
                child: Stack(
                  alignment: Alignment.center,
                  children: [
                    const Text(
                      'Affiliates Dashboard',
                      style: TextStyle(
                        fontSize: 20,
                        fontWeight: FontWeight.w700,
                        color: Colors.white,
                        letterSpacing: -0.27,
                      ),
                    ),
                  
                  ],
                ),
              ),

              // Scrollable Content
              Expanded(
                child: SingleChildScrollView(
                  padding: const EdgeInsets.symmetric(horizontal: 16),
                  child: Column(
                    children: [
                      // Create New Order Button
                      Container(
                        height: 101,
                        width: double.infinity,
                        margin: const EdgeInsets.only(bottom: 20),
                        decoration: BoxDecoration(
                          color: Colors.white.withOpacity(0.05),
                          borderRadius: BorderRadius.circular(12),
                          boxShadow: [
                            BoxShadow(
                              color: Colors.black.withOpacity(0.1),
                              offset: const Offset(0, 4),
                              blurRadius: 6,
                            ),
                          ],
                        ),
                        child: Material(
                          color: Colors.transparent,
                          child: InkWell(
                            onTap: () {},
                            borderRadius: BorderRadius.circular(12),
                            child: Column(
                              mainAxisAlignment: MainAxisAlignment.center,
                              children: const [
                                Icon(Icons.add_circle_outline,
                                    size: 28, color: Colors.white),
                                SizedBox(height: 8),
                                Text(
                                  'Create New Order',
                                  style: TextStyle(
                                    fontSize: 16,
                                    fontWeight: FontWeight.w500,
                                    color: Colors.white,
                                  ),
                                ),
                              ],
                            ),
                          ),
                        ),
                      ),

                      // Details Header
                      Padding(
                        padding: const EdgeInsets.only(bottom: 20),
                        child: Row(
                          mainAxisAlignment: MainAxisAlignment.spaceBetween,
                          children: [
                            const Text(
                              'Details',
                              style: TextStyle(
                                fontSize: 17.5,
                                fontWeight: FontWeight.w700,
                                color: Colors.white,
                              ),
                            ),
                            GestureDetector(
                              onTap: () {},
                              child: Row(
                                children: const [
                                  Text(
                                    'All Status',
                                    style: TextStyle(
                                      fontSize: 13.6,
                                      color: Colors.white,
                                    ),
                                  ),
                                  SizedBox(width: 8),
                                  Icon(Icons.keyboard_arrow_down,
                                      size: 16, color: Color(0xFF6B7280)),
                                ],
                              ),
                            ),
                          ],
                        ),
                      ),

                      // Orders List
                      ...orders.map((order) => OrderCard(order: order)),

                      const SizedBox(height: 20), // Bottom padding for scroll
                    ],
                  ),
                ),
              ),

              // Manage Withdrawals Button
              Padding(
                padding:
                    const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
                child: Container(
                  height: 48,
                  decoration: BoxDecoration(
                    color: const Color(0xFF2160F2),
                    borderRadius: BorderRadius.circular(8),
                    boxShadow: [
                      BoxShadow(
                        color: Colors.black.withOpacity(0.25),
                        offset: const Offset(0, 4),
                        blurRadius: 6,
                      ),
                    ],
                  ),
                  child: Material(
                    color: Colors.transparent,
                    child: InkWell(
                      onTap: () {},
                      borderRadius: BorderRadius.circular(8),
                      child: Row(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: const [
                          Icon(Icons.account_balance_wallet_outlined,
                              color: Colors.white, size: 24),
                          SizedBox(width: 10),
                          Text(
                            'Manage Withdrawals',
                            style: TextStyle(
                              fontSize: 16,
                              fontWeight: FontWeight.w700,
                              color: Colors.white,
                              letterSpacing: 0.21,
                            ),
                          ),
                        ],
                      ),
                    ),
                  ),
                ),
              ),

              // Bottom Navigation
              Container(
                height: 67,
                decoration: BoxDecoration(
                  color: const Color(0xFF182134),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.25),
                      offset: const Offset(0, -3),
                      blurRadius: 6,
                    ),
                  ],
                ),
                child: Row(
                  children: const [
                    NavItem(
                        title: 'Home',
                        icon: Icons.home_outlined,
                        isActive: true),
                    NavItem(
                        title: 'Products', icon: Icons.inventory_2_outlined),
                    NavItem(
                        title: 'Withdrawal',
                        icon: Icons.account_balance_wallet_outlined),
                    NavItem(title: 'Profile', icon: Icons.person_outline),
                  ],
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

