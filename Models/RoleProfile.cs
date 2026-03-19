using Microsoft.AspNetCore.Identity;

namespace EmployeesManagement.Models
{
    public class RoleProfile
    {
        public int id { set; get; }
        public int TaskId { set; get; }
        public SystemProfile Task { set; get; }
        public string RoleId { set; get; }
        public IdentityRole Role { set; get; }



    }
}