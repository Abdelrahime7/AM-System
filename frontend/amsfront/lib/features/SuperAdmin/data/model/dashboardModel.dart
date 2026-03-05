

class DashboardModel
{
final double totalSales ;
final int activeAffiliates;
final int pendingOrders;
final double totalRevenue;
final String userRecent;
final String financRecent;
final String productRecent;

  DashboardModel({required this.totalSales, required this.activeAffiliates,
   required this.pendingOrders, required this.totalRevenue, required this.userRecent, required this.financRecent, required this.productRecent});

factory DashboardModel.fromJson(Map<String, dynamic> json) {
  return DashboardModel(
    totalSales: (json['totalSales'] as num).toDouble(),
    activeAffiliates: json['activeAffiliates'] as int,
    pendingOrders: json['pendingOrders'] as int,
    totalRevenue: (json['totalRevenue'] as num).toDouble(),
    userRecent: json['userRecent'] as String,
    financRecent: json['financRecent'] as String,
    productRecent: json['productRecent'] as String,
  );
}

} 

