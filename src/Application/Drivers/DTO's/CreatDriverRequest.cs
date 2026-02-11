


using Application.RoleRequeste;

namespace Application.Drivers.DTO_s
{
    public class CreateDriverRequest:Role
    {
        public bool ?IsLocal { get; set; }
        public bool ?IsAvailable { get; set; }
       // public int UserID { get; set; }
     
    }
}
