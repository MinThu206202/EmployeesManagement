using System.ComponentModel;
using Humanizer;

namespace EmployeesManagement.Models;

public class ApprovalEntry
{
    public int id { set; get; }
    [DisplayName("Record Id")]
    public int RecordId { set; get; }
    [DisplayName("Document Type")]
    public int DocumentTypeId { set; get; }
    public SystemCodeDetail DocumentType { set; get; }
    [DisplayName("Sequence No")]
    public int SequenceNo { set; get; }
    [DisplayName("Approver Name")]
    public string ApproveId { set; get; }
    public ApplicationUser Approver { set; get; }
    [DisplayName("Status")]
    public int StatusId { set; get; }
    public SystemCodeDetail Status { set; get; }
    [DisplayName("Date Sent For Approval")]
    public DateTime DateSentForApproval { set; get; }
    [DisplayName("Last Modified On")]
    public DateTime LastModifiedOn { set; get; }
    [DisplayName("Last Modified By")]
    public string LastModifiedById { set; get; }
    public ApplicationUser LastModifiedBy { set; get; }
    [DisplayName("Comment")]
    public string Comments { set; get; }
}