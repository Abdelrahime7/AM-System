

using Application.Affiliates.DTO_s.session;
using Application.Common.Models;

namespace Application.Interfaces.AffiliateInterfaces
{
    public interface IAffiliateQueries
    {
        public Task<Result<AffiliateSessionResponse>> GetById(int id);
        public Task<Result<IEnumerable<AffiliateSessionResponse>>> GetAllAffiliates();
    }
}
