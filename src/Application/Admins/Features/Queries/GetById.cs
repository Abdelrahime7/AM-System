using Application.Admins.DTO_s.session;
using Application.Common.Models;
using Application.Interfaces.AdminInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Admins.Features.Queries
{
    partial class AdminQueries() : IAdminQueries
    {
        public Task<Result<IEnumerable<AdminSessionResponse>>> GetAllAdmins()
        {
            throw new NotImplementedException();
        }

        public Task<Result<AdminSessionResponse>> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
