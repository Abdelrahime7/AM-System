

using Application.Admins.DTO_s.session;
using Application.Common.Models;

namespace Application.Admins.Features.Queries
{
    partial class AdminQueries
    {

        public async  Task<Result<IEnumerable<AdminSessionResponse>>> GetAllAdmins()
        {
            try
            {
                var Admins = await _repository.GetAllAsync();
                if (!Admins.Any())
                    return Result<IEnumerable<AdminSessionResponse>>.Failure("No Admins Found");



                var responses = new List<AdminSessionResponse>();


                foreach (var Admin in Admins)
                {
                    var response = await GetById(Admin.Id);
                    responses.Add(response.Value);
                }

                return Result<IEnumerable<AdminSessionResponse>>.Success(responses);

            }
            catch (Exception ex)
            {
                return Result<IEnumerable<AdminSessionResponse>>.Failure("Failled to fetch Admins");
            }
        }


    }
}
