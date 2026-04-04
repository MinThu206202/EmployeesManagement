namespace EmployeesManagement.Models
{
    public class LeavePeriod : UserActivity
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public bool Closed { set; get; }
        public bool Locked { set; get; }
    }
}