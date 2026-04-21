using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EmployeesManagement.Models
{
    public class SystemCodeDetail : UserActivity
    {
        [Key]
        public int Id { set; get; }
        [DisplayName("System Code")]
        public int SystemCodeId { set; get; }
        public SystemCode SystemCode { set; get; }
        [DisplayName("Code")]
        public string Code { set; get; }
        [DisplayName("Description")]
        public string Description { set; get; }
        [DisplayName("Order No")]
        public int? OrderNo { set; get; }

    }
}