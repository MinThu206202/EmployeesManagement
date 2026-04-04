namespace EmployeesManagement.Models
{
    public class LeaveAdjustmentEntry
    {
        public int Id { get; set; }  // Renamed from 'id'
        public int? LeavePeriodId { get; set; }
        public LeavePeriod LeavePeriod { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public decimal NoOfDays { get; set; }
        public DateTime LeaveAdjustmentDate { get; set; }
        public DateTime? LeaveStartDate { get; set; } // nullable optional
        public DateTime? LeaveEndDate { get; set; }   // nullable optional
        public string AdjustmentDescription { get; set; } // fixed typo
        public int AdjustmentTypeId { get; set; }
        public SystemCodeDetail AdjustmentType { get; set; }
    }
}