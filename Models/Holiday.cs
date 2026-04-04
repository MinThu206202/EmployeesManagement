namespace EmployeesManagement.Models
{
    public class Holiday : UserActivity
    {
        public int id { set; get; }
        public string Title { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public String Description { set; get; }
    }
}