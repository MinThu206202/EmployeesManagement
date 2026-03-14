namespace EmployeesManagement.Models
{
    public class Bank : UserActivity
    {
        public int Id { set; get; }
        public string Code { set; get; }

        public string Name { set; get; }
        public string AccountNo { set; get; }

    }
}