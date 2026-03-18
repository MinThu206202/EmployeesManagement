using System.ComponentModel.DataAnnotations;
using Humanizer;

namespace EmployeesManagement.Models
{
    public class LeaveApplication : ApprovalActivity
    {
        public int Id { set; get; }
        [Display(Name = "Employee Name")]
        public int EmployeeId { set; get; }
        public Employee? Employee { set; get; }
        [Display(Name = "No of Leave Days ")]
        public int NoOfDays { set; get; }
        [Display(Name = "Start Date")]

        public DateTime StartDate { set; get; }
        [Display(Name = "End Date")]
        public DateTime EndDate { set; get; }
        [Display(Name = "Leave Duration")]
        public int DurationId { set; get; }
        public SystemCodeDetail? Duration { get; set; }
        [Display(Name = "Leave Type")]
        public int LeaveTypeId { set; get; }
        public LeaveType? LeaveType { get; set; }

        public string? Attachment { get; set; }
        [Display(Name = "Notes")]
        public string Description { get; set; }
        [Display(Name = "Status")]
        public int StatusId { get; set; }
        public SystemCodeDetail? Status { get; set; }
        [Display(Name = "Approval Notes")]
        public string ApprovalNotes { get; set; }
    }
}