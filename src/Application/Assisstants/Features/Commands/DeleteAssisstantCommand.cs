using Application.Common.Models;


namespace Application.Assisstants.Features.Commands
{
    partial class AssisstantCommands
    {
        public async Task<Result<bool>> DeleteAssisstantAsnc(int Id)
        {
            try
            {
                var Assisstant = await _repository.GetByIdAsync(Id);
                if (Assisstant == null)
                    return Result<bool>.Failure("Assisstant Not Found");

                _repository.Delete(Assisstant);
                return Result<bool>.Success(true);
            }
            catch (Exception e)
            {
                return Result<bool>.Failure($"Error Deleting Assisstant: {e.Message}");
            }
        }
    }
}
