namespace EmployeesManagement.Models
{
    public class Employee : UserActivity
    {
        public int Id { set; get; }
        public string EmpNo { set; get; }

        public string FirstName { set; get; }
        public string MiddleName { set; get; }
        public string LastName { set; get; }
        public string FullName => $"{FirstName} {MiddleName} {LastName}";
        public int PhoneNumber { set; get; }
        public string EmailAddress { set; get; }
        public string Country { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string Address { set; get; }
        public string Department { set; get; }
        public string Designation { set; get; }

    }
}