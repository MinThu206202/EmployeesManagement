using Humanizer;

namespace EmployeesManagement.Models
{
    public class ApprovalsUserMatrix : UserActivity
    {
        public int Id { set; get; }
        public string UserId { set; get; }
        public ApplicationUser User { set; get; }

        public int DocumentTypeId { set; get; }
        public SystemCodeDetail DocumentType { set; get; }

        public int WorkFlowUserGroupId { set; get; }
        public WorkFlowUserGroup WorkFlowUserGroup { set; get; }
        public bool Active { set; get; }
    }
}