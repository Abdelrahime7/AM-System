using Application.Users.CredentialChecker;
using Application.Users.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.security.CredentialChecker
{
    public class CredentialChecker : ICredentialChecker
    {
       
         
            private readonly AppDbContext _DbContext;
            private readonly IPasswordHasher<User> _passwordHasher;

            public CredentialChecker(AppDbContext DbContext, IPasswordHasher<User> passwordHasher)
            {
                 _DbContext = DbContext;
                _passwordHasher = passwordHasher;
            }

            public async Task<UserIdentity?> CheckCredentialsAsync(string username, string password)
            {
            var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return null;

            // Verify password
            var result = _passwordHasher.VerifyHashedPassword(user,user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed) return null; 
            // Return identity if valid

               return new UserIdentity(user.Id, user.Role,user.Status);
             }
        }

  }
