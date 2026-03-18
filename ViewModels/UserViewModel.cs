using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace EmployeesManagement.ViewModels
{
    public class UserViewModel()
    {
        public int Id { set; get; }
        [DisplayName("Email Address")]
        public string Email { set; get; }
        [DisplayName("First Name")]
        public string FirstName { set; get; }
        [DisplayName("Middle Name")]
        public string MiddleName { set; get; }
        [DisplayName("Last Name")]
        public string LastName { set; get; }
        [DisplayName("Phone Number")]
        public string PhoneNumber { set; get; }
        [DisplayName("Password")]
        public string Password { set; get; }
        [DisplayName("Address")]
        public string Address { set; get; }
        [DisplayName("User Name")]
        public string UserName { set; get; }
        [DisplayName("National Id")]
        public string? NationalId { set; get; }
        [DisplayName("User Role")]
        public string? RoleId { set; get; }
        public string? FullName => $"{FirstName} {MiddleName} {LastName}";

    }
}