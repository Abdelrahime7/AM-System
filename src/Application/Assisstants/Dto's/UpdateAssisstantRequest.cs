

namespace Application.Assisstants.Dto_s
{
    public class UpdateAssisstantRequest
    {
        public int Id { get; set; } 
        public int? UserId { get; set; }
        public int? AssignedBy { get; set; }
    }
}
