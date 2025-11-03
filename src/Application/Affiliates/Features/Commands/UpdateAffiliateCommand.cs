using Application.Affiliates.DTO_s.session;
using Application.Common.Models;


namespace Application.Affiliates.Features.Commands
{
    partial class AffiliateCommands
    {

        public async Task<Result<bool>> UpdateAffiliateAsnc(UpdateAffiliateSession request)
        {
            try
            {
                var Affiliate = await _repository.GetByIdAsync(request.AffiliateRequest.Id);

                if (Affiliate == null)
                    return Result<bool>.Failure("Affiliate not found");

                if (request.UserRequest != null)
                {
                    await _userCommands.UpdateUserAsync(request.UserRequest);
                }
                _mapper.ToUpdateEntity(Affiliate, request.AffiliateRequest);

                _repository.Update(Affiliate);
                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure($"Failed to update Affiliate: {e.Message}");
            }
        }
    }
}
