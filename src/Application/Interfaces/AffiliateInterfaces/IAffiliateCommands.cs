

using Application.Affiliates.DTO_s.session;
using Application.Common.Models;

namespace Application.Interfaces.AffiliateInterfaces
{
    public interface IAffiliateCommands
    {
        public Task<Result<int>> CreateAffiliateAsync(CreatAffiliateSession request);
        public Task<Result<bool>> DeleteAffiliateAsnc(int Id);
        public Task<Result<bool>> UpdateAffiliateAsnc(UpdateAffiliateSession request);

    }
}
