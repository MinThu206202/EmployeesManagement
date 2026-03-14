using System.ComponentModel.DataAnnotations;

namespace EmployeesManagement.Models
{
    public class SystemCodeDetail : UserActivity
    {
        [Key]
        public int Id { set; get; }
        public int SystemCodeId { set; get; }
        public SystemCode SystemCode { set; get; }
        public string Code { set; get; }
        public string Description { set; get; }
        public int? OrderNo { set; get; }

    }
}