

using Application.RoleRequeste;

namespace Application.Assisstants.Dto_s
{
    public class CreatAssisstantRequest:Role
    {
      //  public int UserId {  get; set; }
        public int  ?AssignedBy { get; set; }
        
    }
}
