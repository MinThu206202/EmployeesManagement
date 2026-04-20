using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace EmployeesManagement.Models
{
    public class Holiday : UserActivity
    {
        [DisplayName("No")]
        public int id { set; get; }
        [DisplayName("Holiday Name")]
        public string Title { set; get; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("Start Date")]

        public DateTime StartDate { set; get; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        [DisplayName("End Date")]

        public DateTime EndDate { set; get; }
        public String Description { set; get; }
    }
}