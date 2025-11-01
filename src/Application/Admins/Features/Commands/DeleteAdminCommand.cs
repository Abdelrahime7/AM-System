using Application.Common.Models;


namespace Application.Admins.Features.Commands
{
    partial class AdminCommands
    {
        public async Task<Result<bool>> DeleteAdminAsnc(int Id)
        {
            try
            {
                var Admin = await _repository.GetByIdAsync(Id);
                if (Admin == null)
                    return Result<bool>.Failure("Admin Not Found");

                _repository.Delete(Admin);
                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure($"Error Deleting Admin: {e.Message}");
            }
        }
    }
}
