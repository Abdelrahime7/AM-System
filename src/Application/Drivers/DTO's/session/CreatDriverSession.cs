using Application.Users.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Drivers.DTO_s.session
{
    public class CreatDriverSession
    {
        public required CreateUserRequest UserRequest { get; set; }
        public required CreateDriverRequest DriverRequest { get; set; }
    }
}
