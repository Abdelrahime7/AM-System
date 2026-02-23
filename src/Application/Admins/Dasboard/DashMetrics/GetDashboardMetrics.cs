

using Application.Admins.Dasboard.DashDto;
using Application.Interfaces.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Admins.Dasboard.DashMetrics
{
    
        namespace Application.Admin.Dashboard
    {
        public class GetDashboardMetrics
        {
            private readonly IAffiliateRepository _affiliates;
            private readonly IOrderRepository _orders;
           

            public GetDashboardMetrics(
                IAffiliateRepository affiliates,
                IOrderRepository orders)
            {
                _affiliates = affiliates;
                _orders = orders;
                
            }

          
               public async Task<DashboardDto> Execute()
            {

                var affiliatesTask =await _affiliates.CountActiveAsync();
                var ordersTask = await _orders.CountPendingAsync();
                var totalSales = await _orders.TotalSalesAsync();

                return new DashboardDto(
                    totalSales: totalSales,
                    activeAffiliates: affiliatesTask,
                    pendingOrders: ordersTask,
                    totalRevenue: null
                );
            }

        }
    }
    

}
