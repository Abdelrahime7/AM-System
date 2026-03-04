




class DashboardState {
  final bool isLoading;
  final String? error;
  final double totalSales;
  final int activeAffiliates;
  final int pendingOrders;
  final double totalRevenue;
  final String userRecent;
  final String financRecent;
  final String productRecent;

  DashboardState({
    this.isLoading = false,
    this.error,
    this.totalSales = 0,
    this.activeAffiliates = 0,
    this.pendingOrders = 0,
    this.totalRevenue = 0,
    this.userRecent="",
    this.financRecent="",
    this.productRecent="",
  });

  DashboardState copyWith({
    bool? isLoading,
    String? error,
    double? totalSales,
    int? activeAffiliates,
    int? pendingOrders,
    double? totalRevenue,
    String ? userRecent,
    String ? financRecent,
    String ? productRecent,
  }) {
    return DashboardState(
      isLoading: isLoading ?? this.isLoading,
      error: error ?? this.error,
      totalSales: totalSales ?? this.totalSales,
      activeAffiliates: activeAffiliates ?? this.activeAffiliates,
      pendingOrders: pendingOrders ?? this.pendingOrders,
      totalRevenue: totalRevenue ?? this.totalRevenue,
      userRecent: userRecent ?? this.userRecent,
      financRecent: financRecent ?? this.financRecent,
      productRecent: productRecent ?? this.productRecent, 
    );
  }
}

