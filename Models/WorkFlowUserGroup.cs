using System.ComponentModel;

namespace EmployeesManagement.Models
{
    public class WorkFlowUserGroup
    {
        public int Id { set; get; }
        public string Code { set; get; }
        public string Description { set; get; }
        public int? DepartmentId { set; get; }
        public Department Department { set; get; }
        public int? DocumentTypeId { set; get; }
        public SystemCodeDetail DocumentType { set; get; }

    }

    public class WorkFlowUserGroupMember
    {
        public int Id { set; get; }
        public int WorkFlowUserGroupId { set; get; }

        public WorkFlowUserGroup WorkFlowUserGroup { set; get; }


        [DisplayName("Document Sender")]
        public string SenderId { set; get; }
        public ApplicationUser Sender { set; get; }
        [DisplayName("Document Approver")]
        public string ApproverId { set; get; }
        public ApplicationUser Approver { set; get; }
        public int SequenceNo { set; get; }
    }
}