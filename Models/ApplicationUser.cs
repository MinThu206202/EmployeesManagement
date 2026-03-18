using Microsoft.AspNetCore.Identity;

namespace EmployeesManagement.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { set; get; }
        public string? MiddleName { set; get; }
        public string? LastName { set; get; }
        public string? NationalId { set; get; }
        public string? CreatedById { set; get; }
        public DateTime? CreatedOn { set; get; }
        public string? FullName => $"{FirstName} {MiddleName} {LastName}";
        public DateTime? LoginDate { set; get; }
        public string? ModifiedById { set; get; }
        public DateTime? ModifiedOn { set; get; }
        public DateTime? PasswordChangedOn { set; get; }
        public string? RoleId { set; get; }
        public IdentityRole Role { set; get; }
    }
}