using System.ComponentModel.DataAnnotations;

namespace EmployeesManagement.Models
{
    public class SystemCode : UserActivity
    {
        [Key]
        public int Id { set; get; }
        public string Code { set; get; }
        public string Description { set; get; }
    }

}