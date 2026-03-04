using Domain.Enums;


namespace Domain.Entities
{
    public class Admin
    {
       public int Id { get; set; }
        public  AccessLevels access { get ; set; }
        public int UserID { get; set; }
        public User user { get; set; }
         
    }
}
