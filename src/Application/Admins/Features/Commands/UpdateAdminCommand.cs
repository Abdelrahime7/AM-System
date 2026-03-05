using Application.Admins.DTO_s.session;
using Application.Common.Models;


namespace Application.Admins.Features.Commands
{
    partial class AdminCommands
    {

        public async Task<Result<bool>> UpdateAdminAsnc(UpdateAdminSession request)
        {
            try
            {
                var Admin = await _repository.GetByIdAsync(request.AdminRequest.Id);

                if (Admin == null)
                    return Result<bool>.Failure("Admin not found");

                if (request.UserRequest != null)
                {
                    await _userCommands.UpdateUserAsync(request.UserRequest);
                }
                _mapper.ToUpdateEntity(Admin, request.AdminRequest);

                _repository.Update(Admin);
                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure($"Failed to update Admin: {e.Message}");
            }
        }
    }
}
