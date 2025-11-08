

namespace Domain.Entities
{
    public class Assisstant
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AssignedBy { get; set; }   
        public User User { get; set; }
       
    }
}
