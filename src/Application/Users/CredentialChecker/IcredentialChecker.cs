using Application.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.CredentialChecker
{
    public interface ICredentialChecker
    {
        Task<UserIdentity?> CheckCredentialsAsync(string username, string password);
    }

}
