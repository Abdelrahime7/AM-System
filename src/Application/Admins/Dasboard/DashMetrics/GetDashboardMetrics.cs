

using Application.Admins.Dasboard.DashDto;
using Application.Interfaces.Repositories;


namespace Application.Admins.Dasboard.DashMetrics
{
    
        namespace Application.Admin.Dashboard
    {
        public class GetDashboardMetrics(
            IAffiliateRepository affiliates,
            IOrderRepository orders, IDriverRepository drivers,
            IAssisstantRepository assisstants,
            IAdminRepository admins,
            IUserRepository users,
            IWithdrawalRepository withdrawals,
            IProductRepository products
            )
        {
            private readonly IAffiliateRepository _affiliates = affiliates;
            private readonly IDriverRepository _drivers= drivers;
            private readonly IAssisstantRepository _assisstants= assisstants;
            private readonly IAdminRepository _admins= admins;
            private readonly IOrderRepository _orders = orders;
            private readonly IUserRepository _user = users;
            private readonly IWithdrawalRepository _withdrawals = withdrawals;
            private readonly IProductRepository _products = products;

            public int affiliates { get; set; }
            public int orders { get; set; }
            public decimal totalSales {  get; set; }

            // In most business apps, sequential async queries are fine because the database engine itself is optimized.
            // Parallelism only helps if each query is heavy and independent.
            public async Task<DashboardDto> Execute()
            {
               
                var pendingUsers = await getPendigUsers();

                var userRecent = await _user.GetRecentUserAsync();
                var financRecent = await _withdrawals.GetRecentWithdrawelAsync();
                var  productRecent=await _products .GetRecentProductAsync();

                return new DashboardDto(

                    totalSales: totalSales,
                    activeAffiliates: affiliates,
                    userRecent: userRecent,
                    financRecent : financRecent,
                    productRecent : productRecent,
                    pendingOrders: orders,
                    totalRevenue: null
                );
            }

         public async Task <int> getPendigUsers() => await _drivers.CountPendingAsync() +
                 await _affiliates.CountPendingAsync() +
                 await _admins.CountPendingAsync() +
                 await _assisstants.CountPendingAsync();
             
         public async Task statsData()
            {

                affiliates = await _affiliates.CountActiveAsync();
                var orders = await _orders.CountPendingAsync();
                var totalSales = await _orders.TotalSalesAsync();
            }




        }
    }
    

}
