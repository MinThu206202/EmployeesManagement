using Humanizer;

namespace EmployeesManagement.Models
{
    public class LeaveType : UserActivity
    {
        public int Id { set; get; }
        public string Code { set; get; }
        public string Name { set; get; }
    }
}