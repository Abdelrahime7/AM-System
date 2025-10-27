using Application.Admins.Dto_s;
using Application.Users.DTOs;


namespace Application.Drivers.DTO_s.session
{
    public class AdminSessionResponse
    {
        public UserResponse UserResponse { get; set; }
        public AdminResponse AdminResponse {get; set; }
    }
}
