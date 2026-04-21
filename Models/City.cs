using System.ComponentModel;
using System.Runtime.InteropServices;

namespace EmployeesManagement.Models
{
    public class City
    {
        public int Id { set; get; }
        [DisplayName("City Code")]
        public string Code { set; get; }
        [DisplayName("City Name")]
        public string Name { set; get; }
        [DisplayName("Country Name")]
        public int CountryId { set; get; }
        public Country Country { set; get; }
    }
}