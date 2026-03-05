using Application.Users.DTOs;

namespace Application.Affiliates.DTO_s.session
{
    public class CreatAffiliateSession
    {
        public required CreateUserRequest UserRequest { get; set; }
        public required CreateAffiliateRequest AffiliateRequest { get; set; }
    }
}
