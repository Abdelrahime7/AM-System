

namespace Application.Admins.Dasboard.DashDto
{

  
        public class DashboardDto
        {
            public Decimal TotalSales { get; set; }
            public int ActiveAffiliates { get; set; }
            public int PendingOrders { get; set; }
            public Decimal TotalRevenue { get; set; }

            public DashboardDto(
                Decimal? totalSales,
                int? activeAffiliates,
                int ?pendingOrders,
                Decimal ?totalRevenue)
            {
            TotalSales = totalSales ?? 1000m;
                ActiveAffiliates = activeAffiliates ?? 120 ;
                PendingOrders =  pendingOrders ?? 50;
                TotalRevenue = totalRevenue ?? 190.00m;
            }
        }
 }
