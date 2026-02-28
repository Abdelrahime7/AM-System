using Application.Admins.Dasboard.DashDto;
using Application.Admins.Dasboard.DashMetrics.Application.Admin.Dashboard;
using HotChocolate.Authorization;


namespace WebAPI.Graphql
{
    public class Query
    {
       // [Authorize(Policy = "SuperAdminOnly")]

        public async Task<DashboardDto> GetDashboard([Service] GetDashboardMetrics useCase)
        {
            return await useCase.Execute();
        }
    }

}
