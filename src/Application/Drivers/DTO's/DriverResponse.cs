using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.DTO_s
{
    public class DriverResponse
    {
      
        public bool IsLocal { get; set; }
        public bool IsAvailable { get; set; }
        public int UserID { get; set; }
      

    }
}
