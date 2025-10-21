using Application.Users.DTOs;


namespace Application.Drivers.DTO_s.session
{
    public class DriverSessionResponse
    {
        public UserResponse UserResponse { get; set; }
        public DriverResponse DriverResponse {get; set; }
    }
}
