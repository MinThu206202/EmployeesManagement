namespace EmployeesManagement.Models
{
    public class SystemProfile : UserActivity
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public int? ProfileId { set; get; }
        public SystemProfile Profile { get; set; }
        public ICollection<SystemProfile> Children { set; get; }
        public int? Order { set; get; }
    }
}