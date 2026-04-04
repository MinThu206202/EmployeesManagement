using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EmployeesManagement.Models;
using Humanizer;


namespace EmployeesManagement.ViewModels
{
    public class ProfileViewModel
    {
        public ICollection<SystemProfile> Profiles { set; get; }
        public ICollection<int> RolesRightsIds { set; get; }
        public int[] Ids { get; set; }

        [DisplayName("Role")]
        public string RoleId { set; get; }
        [DisplayName("System Task")]
        public int TaskId { set; get; }


    }
}