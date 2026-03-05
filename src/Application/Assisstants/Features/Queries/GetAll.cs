

using Application.Assisstants.Dto_s.session;
using Application.Common.Models;

namespace Application.Assisstants.Features.Queries
{
  public   partial class AssisstantQueries
    {

        public async  Task<Result<IEnumerable<AssisstantSessionResponse>>> GetAllAssisstants()
        {
            try
            {
                var Assisstants = await _repository.GetAllAsync();
                if (!Assisstants.Any())
                    return Result<IEnumerable<AssisstantSessionResponse>>.Failure("No Assisstants Found");



                var responses = new List<AssisstantSessionResponse>();


                foreach (var Assisstant in Assisstants)
                {
                    var response = await GetById(Assisstant.Id);
                    responses.Add(response.Value);
                }

                return Result<IEnumerable<AssisstantSessionResponse>>.Success(responses);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<AssisstantSessionResponse>>.Failure("Failled to fetch Assisstants");
            }
        }


    }
}
