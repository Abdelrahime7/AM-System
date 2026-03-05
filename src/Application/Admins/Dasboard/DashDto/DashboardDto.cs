



namespace Application.Admins.Dasboard.DashDto
{

  
        public class DashboardDto
        {
            public Decimal TotalSales { get; set; }
            public int ActiveAffiliates { get; set; }
            public int PendingOrders { get; set; }
            public Decimal TotalRevenue { get; set; }
            public string UserRecent { get; set; } 
            public string FinancRecent { get; set; }
           public  string ProductRecent { get; set; }

            public DashboardDto(
                Decimal? totalSales,
                int? activeAffiliates,
                string userRecent,
                 string financRecent,
                 string productRecent,
                int ?pendingOrders,
                Decimal ?totalRevenue)
            {
            TotalSales = totalSales ?? 0.00m;
                ActiveAffiliates = activeAffiliates ?? 0 ;
                PendingOrders =  pendingOrders ?? 00;
                TotalRevenue = totalRevenue ?? 0.00m;
              this. UserRecent = userRecent ?? " ";
               this. FinancRecent= financRecent?? " ";
                this.ProductRecent = productRecent ?? " ";


            }

     
    }
 }
