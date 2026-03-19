namespace EmployeesManagement.Models
{
    public class Audit
    {
        public int id { set; get; }
        public string UserId { set; get; }

        public string AuditType { set; get; }
        public string TableName { set; get; }
        public DateTime DateTime { set; get; }
        public string? OldValues { set; get; }
        public string? NewValues { set; get; }
        public string? AffectedColumns { set; get; }
        public string? Primarykey { set; get; }
    }

    public enum AuditType
    {
        None = 0,
        Create = 1,
        Update = 2,
        Delete = 3,
        Login = 4
    }

}