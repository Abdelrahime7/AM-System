

class DashboardModel
{
final double totalSales ;
final int activeAffiliates;
final int pendingOrders;
final double totalRevenue;

  DashboardModel({required this.totalSales, required this.activeAffiliates,
   required this.pendingOrders, required this.totalRevenue});

factory DashboardModel.fromJson(Map<String, dynamic> json) {
  return DashboardModel(
    totalSales: (json['totalSales'] as num).toDouble(),
    activeAffiliates: json['activeAffiliates'] as int,
    pendingOrders: json['pendingOrders'] as int,
    totalRevenue: (json['totalRevenue'] as num).toDouble(),
  );
}

} 

