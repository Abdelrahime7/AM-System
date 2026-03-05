using Application.Common.Models;


namespace Application.Affiliates.Features.Commands
{
    partial class AffiliateCommands
    {
        public async Task<Result<bool>> DeleteAffiliateAsnc(int Id)
        {
            try
            {
                var Affiliate = await _repository.GetByIdAsync(Id);
                if (Affiliate == null)
                    return Result<bool>.Failure("Affiliate Not Found");

                _repository.Delete(Affiliate);
                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure($"Error Deleting Affiliate: {e.Message}");
            }
        }
    }
}
