import 'package:amsfront/features/Affiiates/presentation/widgets/ordercard.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';


class AssistantScreen extends StatelessWidget {
  const AssistantScreen({super.key});

  @override
  Widget build(BuildContext context) {
    // Mock Data
    final List<Map<String, String>> orders = [
      {'id': '12345', 'customer': 'Layla Hassan', 'amount': '\$150.00'},
      {'id': '12346', 'customer': 'Ahmed Ali', 'amount': '\$150.00'},
      {'id': '12347', 'customer': 'Sara Mohamed', 'amount': '\$150.00'},
      {'id': '12348', 'customer': 'Omar Ibrahim', 'amount': '\$150.00'},
      {'id': '12349', 'customer': 'Fatima Youssef', 'amount': '\$150.00'},
    ];

    return AnnotatedRegion<SystemUiOverlayStyle>(
      value: const SystemUiOverlayStyle(
        statusBarColor: Color(0xFF121721),
        statusBarIconBrightness: Brightness.light,
      ),
      child: Scaffold(
        appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 44, 54, 83),
        elevation: 0,
        iconTheme: const IconThemeData(color: Colors.white),
      
      ),
        backgroundColor: const Color(0xFF121721),
        body: SafeArea(
          child: Column(
            children: [
              // Header 
                
              Container(
                height: 72,
                padding: const EdgeInsets.symmetric(horizontal: 16),
                color: const Color(0xFF121721),
                child: Stack(
                  alignment: Alignment.center,
                  children: [
                    const Text(
                      'Assisstant Dashboard',
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

