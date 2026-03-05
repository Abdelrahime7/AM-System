using Application.Assisstants.Dto_s.session;
using Application.Common.Models;


namespace Application.Assisstants.Features.Commands
{
    partial class AssisstantCommands
    {

        public async Task<Result<bool>> UpdateAssisstantAsnc(UpdateAssisstantSession request)
        {
            try
            {
                var Assisstant = await _repository.GetByIdAsync(request.AssisstantRequest.Id);

                if (Assisstant == null)
                    return Result<bool>.Failure("Assisstant not found");

                if (request.UserRequest != null)
                {
                    await _userCommands.UpdateUserAsync(request.UserRequest);
                }
                _mapper.ToUpdateEntity(Assisstant, request.AssisstantRequest);

                _repository.Update(Assisstant);
                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure($"Failed to update Assisstant: {e.Message}");
            }
        }
    }
}
