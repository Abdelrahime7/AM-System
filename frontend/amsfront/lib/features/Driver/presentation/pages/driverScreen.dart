import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:amsfront/features/Driver/presentation/widgets/ordrercard.dart';
class DriverScreen extends StatelessWidget {
  const DriverScreen({super.key});

  @override
  Widget build(BuildContext context) {
    // Mock Data
    final List<Map<String, dynamic>> orders = [
      {'id': 1, 'customerId': 125, 'orderNumber': 789012, 'customerName': 'Layla Ali'},
      {'id': 2, 'customerId': 125, 'orderNumber': 789012, 'customerName': 'Layla Ali'},
      {'id': 3, 'customerId': 125, 'orderNumber': 789012, 'customerName': 'Layla Ali'},
      {'id': 4, 'customerId': 125, 'orderNumber': 789012, 'customerName': 'Layla Ali'},
      {'id': 5, 'customerId': 125, 'orderNumber': 789012, 'customerName': 'Layla Ali'},
      {'id': 6, 'customerId': 125, 'orderNumber': 789012, 'customerName': 'Layla Ali'},
    ];

    return AnnotatedRegion<SystemUiOverlayStyle>(
      value: const SystemUiOverlayStyle(
        statusBarColor: Color(0xFF0F1724),
        statusBarIconBrightness: Brightness.light,
      ),
      child: Scaffold(
        backgroundColor: const Color(0xFF0F1724),
        appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 44, 54, 83),
        elevation: 0,
        iconTheme: const IconThemeData(color: Colors.white),
      
      ),
        body: SafeArea(
          child: Column(
            children: [
              // Header
             
              Container(
                height: 72,
                width: double.infinity,
                padding: const EdgeInsets.symmetric(horizontal: 16),
                color: const Color(0xFF0F1724),
                child: Stack(
                  alignment: Alignment.center,
                  children: const [
                    Text(
                      'Assigned Orders',
                      style: TextStyle(
                        fontFamily: 'Inter',
                        fontWeight: FontWeight.w700,
                        fontSize: 18,
                        color: Colors.white,
                      ),
                    ),
                  ],
                ),
              ),

              // Orders List
              Expanded(
                child: ListView.builder(
                  padding: const EdgeInsets.symmetric(horizontal: 16),
                  itemCount: orders.length,
                  itemBuilder: (context, index) {
                    final order = orders[index];
                    return OrderCard(order: order);
                  },
                ),
              ),
            ],
          ),
        ),
      ),
    );
  }
}

