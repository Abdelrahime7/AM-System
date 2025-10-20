
namespace Application.Drivers.DTO_s
{
    public class UpdateDriverRequest
    {
        public int Id { get; set; }
        public bool? IsLocal { get; set; }
        public bool? IsAvailable { get; set; }
        public int? UserID { get; set; }

    }
}
