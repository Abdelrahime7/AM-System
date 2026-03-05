

using Application.Affiliates.DTO_s.session;
using Application.Common.Models;

namespace Application.Affiliates.Features.Queries
{
  public   partial class AffiliateQueries
    {

        public async  Task<Result<IEnumerable<AffiliateSessionResponse>>> GetAllAffiliates()
        {
            try
            {
                var Affiliates = await _repository.GetAllAsync();
                if (!Affiliates.Any())
                    return Result<IEnumerable<AffiliateSessionResponse>>.Failure("No Affiliates Found");



                var responses = new List<AffiliateSessionResponse>();


                foreach (var Affiliate in Affiliates)
                {
                    var response = await GetById(Affiliate.Id);
                    responses.Add(response.Value);
                }

                return Result<IEnumerable<AffiliateSessionResponse>>.Success(responses);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<AffiliateSessionResponse>>.Failure("Failled to fetch Affiliates");
            }
        }


    }
}
