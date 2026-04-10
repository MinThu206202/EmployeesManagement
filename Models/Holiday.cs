using System.ComponentModel.DataAnnotations;
namespace EmployeesManagement.Models
{
    public class Holiday : UserActivity
    {
        public int id { set; get; }
        public string Title { set; get; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]

        public DateTime StartDate { set; get; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]

        public DateTime EndDate { set; get; }
        public String Description { set; get; }
    }
}