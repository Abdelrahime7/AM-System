using Domain.Enums;


namespace Application.Users.DTOs
{
    public class ChangeStatusRequest
    {
        public int userID {  get; set; }
        public UserStatus status { get; set; }
    }
}
