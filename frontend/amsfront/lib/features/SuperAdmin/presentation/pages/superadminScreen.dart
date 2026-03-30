import 'package:amsfront/app/di/riverpod_di/provider.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:amsfront/features/SuperAdmin/presentation/widgets/navitem.dart';
import 'package:amsfront/features/SuperAdmin/presentation/widgets/starcard.dart';
import 'package:amsfront/features/SuperAdmin/presentation/widgets/actioncard.dart';
import 'package:amsfront/features/SuperAdmin/presentation/widgets/activeitem.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:go_router/go_router.dart';

class SuperAdminScreen extends ConsumerStatefulWidget {
  const SuperAdminScreen({super.key});

  @override
  ConsumerState<SuperAdminScreen> createState() => _SuperAdminScreenState();
}

class _SuperAdminScreenState extends ConsumerState<SuperAdminScreen> {
  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addPostFrameCallback((_) {
      ref.read(dashboardProvider.notifier).loadDashboardData();
    });
  }

  @override
  Widget build(BuildContext context) {
    final dashboard = ref.watch(dashboardProvider);
     
    final List<Map<String, String>> statsData = [
      {'title': 'Total Sales', 'value': dashboard.totalSales.toStringAsFixed(0)},
      {'title': 'Active Marketers', 'value': dashboard.activeAffiliates.toString()},
      {'title': 'Pending Orders', 'value': dashboard.pendingOrders.toString()},
      {'title': 'Total Revenue', 'value': '\$${dashboard.totalRevenue.toStringAsFixed(2)}'},
    ];

    final List<Map<String, dynamic>> quickActions = [
      {'title': 'Manage Users', 'icon': Icons.people_outline},
      {'title': 'Manage Products', 'icon': Icons.inventory_2_outlined},
      {'title': 'Manage Finance', 'icon': Icons.credit_card_outlined},
      {'title': 'View Reports', 'icon': Icons.bar_chart_outlined},
    ];

    final List<Map<String, dynamic>> recentActivity = [
      {
        'type': 'New User',
        'description':dashboard.userRecent,
        'icon': Icons.person_add_outlined,
        'color': const Color(0xFF3B82F6),
      },
      {
        'type': 'Withdrawal Request',
        'description':dashboard.financRecent,
        'icon': Icons.credit_card_outlined,
        'color': const Color(0xFF22C55E),
      },
      {
        'type': 'Product Added',
        'description': dashboard.productRecent,
        'icon': Icons.inventory_2_outlined,
        'color': const Color(0xFFA855F7),
      },
    ];

    return AnnotatedRegion<SystemUiOverlayStyle>(
      value: const SystemUiOverlayStyle(
        statusBarColor: Color(0xFF121721),
        statusBarIconBrightness: Brightness.light,
      ),
      child: Scaffold(
        backgroundColor: const Color(0xFF121721),
         appBar: AppBar(
        backgroundColor: const Color.fromARGB(255, 44, 54, 83),
        elevation: 0,
        iconTheme: const IconThemeData(color: Colors.white),
      
      ),
        body: SafeArea(
          child: Column(
      
            children: [
             
            
              

              Container(
                height: 60,
                padding: const EdgeInsets.symmetric(horizontal: 16, vertical: 8),
                color: const Color(0xFF121721),
                child: Stack(
                  alignment: Alignment.center,
                  children: [
                    const Text(
                      'Admin dashboard',
                      style: TextStyle(
                        fontSize: 18,
                        fontWeight: FontWeight.w700,
                        color: Colors.white,
                      ),
                    ),
                  
               
                  ],
                ),
              ),

              // Scrollable Content
              Expanded(
                child: SingleChildScrollView(
                  child: Column(
                    crossAxisAlignment: CrossAxisAlignment.start,
                    children: [
                                 // Header
                                 
        if (dashboard.isLoading) 
          const Padding(
            padding: EdgeInsets.all(20.0),
            child: Center(child: CircularProgressIndicator()),
          ), 
        if (dashboard.error != null) 
          Center(child: Text("Error: ${dashboard.error}", style: const TextStyle(color: Colors.red))),
           
                      const Padding(
                        padding: EdgeInsets.fromLTRB(16, 20, 16, 8),
                        child: Text(
                          'Welcome back',
                          style: TextStyle(
                            fontSize: 24,
                            fontWeight: FontWeight.w700,
                            color: Colors.white,
                          ),
                        ),
                      ),

                      // Stats Grid
                      Padding(
                        padding: const EdgeInsets.symmetric(horizontal: 16),
                        child: Column(
                          children: [
                            Row(
                              children: [
                                Expanded(child: StatCard(data: statsData[0])),
                                const SizedBox(width: 16),
                                Expanded(child: StatCard(data: statsData[1])),
                              ],
                            ),
                            const SizedBox(height: 16),
                            Row(
                              children: [
                                Expanded(child: StatCard(data: statsData[2])),
                                const SizedBox(width: 16),
                                Expanded(child: StatCard(data: statsData[3])),
                              ],
                            ),
                          ],
                        ),
                      ),

                      const SizedBox(height: 20),

                      // Quick Actions
                      const Padding(
                        padding: EdgeInsets.symmetric(horizontal: 16, vertical: 12),
                        child: Text(
                          'Quick Actions',
                          style: TextStyle(
                            fontSize: 22,
                            fontWeight: FontWeight.w700,
                            color: Colors.white,
                          ),
                        ),
                      ),

                      Padding(
                        padding: const EdgeInsets.symmetric(horizontal: 16),
                        child: Column(
                          children: [
                            Row(
                              children: [
                                Expanded(child: ActionCard(data: quickActions[0], onPressed: () => context.push('/User-management'))),
                                const SizedBox(width: 12),
                                Expanded(child: ActionCard(data: quickActions[1])),
                              ],
                            ),
                            const SizedBox(height: 12),
                            Row(
                              children: [
                                Expanded(child: ActionCard(data: quickActions[2])),
                                const SizedBox(width: 12),
                                Expanded(child: ActionCard(data: quickActions[3])),
                              ],
                            ),
                          ],
                        ),
                      ),

                      const SizedBox(height: 20),

                      // Recent Activity
                      const Padding(
                        padding: EdgeInsets.symmetric(horizontal: 16, vertical: 12),
                        child: Text(
                          'Recent Activity',
                          style: TextStyle(
                            fontSize: 22,
                            fontWeight: FontWeight.w700,
                            color: Colors.white,
                          ),
                        ),
                      ),

                      Padding(
                        padding: const EdgeInsets.symmetric(horizontal: 16),
                        child: Column(
                          children: recentActivity
                              .map((activity) => ActivityItem(activity: activity))
                              .toList(),
                        ),
                      ),

                      const SizedBox(height: 20), // Bottom padding
                    ],
                  ),
                ),
              ),

              // Bottom Navigation
              Container(
                padding: const EdgeInsets.symmetric(vertical: 9, horizontal: 16),
                decoration: BoxDecoration(
                  color: const Color(0xFF192433),
                  boxShadow: [
                    BoxShadow(
                      color: Colors.black.withOpacity(0.25),
                      offset: const Offset(0, -3),
                      blurRadius: 6,
                    ),
                  ],
                ),
                child: Row(
                  mainAxisAlignment: MainAxisAlignment.spaceBetween,
                  children: const [
                    NavItem(icon: Icons.home, label: 'Home', isActive: true),
                    NavItem(icon: Icons.people_outline, label: 'Users'),
                    NavItem(icon: Icons.inventory_2_outlined, label: 'Products'),
                    NavItem(icon: Icons.credit_card_outlined, label: 'Finance'),
                    NavItem(icon: Icons.bar_chart_outlined, label: 'Analytics'),
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
