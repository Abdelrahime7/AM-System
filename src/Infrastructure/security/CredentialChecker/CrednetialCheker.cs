using Application.Users.CredentialChecker;
using Application.Users.DTOs;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;


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

          public async Task <List<Claim>> BuildClaims( int UserId)
        {

            var User = await _DbContext.Users.FirstOrDefaultAsync(u => u.Id == UserId);
              if (User == null) return null;
               
            var claims = new List<Claim>
            {

              new Claim(ClaimTypes.NameIdentifier, User.Id.ToString()),
              new Claim(ClaimTypes.Role, User.Role.ToString()),
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()

              )
            };
              return claims;
        }


        }

  }
