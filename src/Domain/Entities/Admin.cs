using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

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
