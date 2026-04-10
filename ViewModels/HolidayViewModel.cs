using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using EmployeesManagement.Models;

namespace EmployeesManagement.ViewModels
{
    public class HolidayViewModel : UserViewModel
    {
        public int id { set; get; }
        [DisplayName("Title")]
        public string Title { set; get; }

        [DisplayName("Start Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime? StartDate { set; get; }

        [DisplayName("End Date")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]

        public DateTime? EndDate { set; get; }
        [DisplayName("Description")]

        public String Description { set; get; }
        public Holiday Holiday { set; get; }
        public List<Holiday> Holidays { set; get; }
    }
}