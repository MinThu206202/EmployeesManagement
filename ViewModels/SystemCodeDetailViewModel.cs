using System.ComponentModel;
using EmployeesManagement.Models;

namespace EmployeesManagement.ViewModels
{
    public class SystemCodeDetailViewMode
    {
        public int id{set;get;}
        [DisplayName("System Code")]
        public int SystemCodeId{set;get;}
        public SystemCode SystemCode{set;get;}
        [DisplayName("Code")]
        public string Code{set;get;}
        [DisplayName("Description Code")]
        public string Description{set;get;}
        [DisplayName("Order NO")]
        public int? OrderNo{set;get;}

        [DisplayName("Created By")]
        public string CreatedById{set;get;}

        [DisplayName("Created On")]
        public string CreatedOn{set;get;}

        public  List<SystemCodeDetail> SystemCodeDetails{set;get;}
    }
}