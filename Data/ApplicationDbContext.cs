using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using EmployeesManagement.Models;

namespace EmployeesManagement.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LeaveApplication>()
                .HasOne(l => l.Duration)
                .WithMany()
                .HasForeignKey(l => l.DurationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<LeaveApplication>()
                .HasOne(l => l.Status)
                .WithMany()
                .HasForeignKey(l => l.StatusId)
                .OnDelete(DeleteBehavior.Restrict);

            // ✅ ADD THIS PART
            modelBuilder.Entity<ApprovalEntry>()
    .HasOne(a => a.Status)
    .WithMany()
    .HasForeignKey(a => a.StatusId)
    .OnDelete(DeleteBehavior.Restrict); // ✅ FIX

            modelBuilder.Entity<ApprovalEntry>()
                .HasOne(a => a.DocumentType)
                .WithMany()
                .HasForeignKey(a => a.DocumentTypeId)
                .OnDelete(DeleteBehavior.Restrict); // ✅ FIX

            // ✅ FIX CompanyInformation relationships
            modelBuilder.Entity<CompanyInformation>()
                .HasOne(c => c.City)
                .WithMany()
                .HasForeignKey(c => c.CityId)
                .OnDelete(DeleteBehavior.Restrict); // 🔥 IMPORTANT

            modelBuilder.Entity<CompanyInformation>()
                .HasOne(c => c.Country)
                .WithMany()
                .HasForeignKey(c => c.CountryId)
                .OnDelete(DeleteBehavior.Restrict); // 🔥 IMPORTANT

            modelBuilder.Entity<WorkFlowUserGroupMember>()
.HasOne(w => w.Sender)
.WithMany()
.HasForeignKey(w => w.SenderId)
.OnDelete(DeleteBehavior.Restrict); // 🔥 FIX

            modelBuilder.Entity<WorkFlowUserGroupMember>()
                .HasOne(w => w.Approver)
                .WithMany()
                .HasForeignKey(w => w.ApproverId)
                .OnDelete(DeleteBehavior.Restrict); // 🔥 FIX

            modelBuilder.Entity<WorkFlowUserGroup>()
    .HasOne(w => w.Department)
    .WithMany()
    .HasForeignKey(w => w.DepartmentId)
    .IsRequired(false) // allow nulls for existing rows
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WorkFlowUserGroup>()
                .HasOne(w => w.DocumentType)
                .WithMany()
                .HasForeignKey(w => w.DocumentTypeId)
                .IsRequired(false) // allow nulls for existing rows
                .OnDelete(DeleteBehavior.Restrict);
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Bank> Banks { get; set; }
        public DbSet<SystemCode> SystemCodes { get; set; }
        public DbSet<SystemCodeDetail> SystemCodeDetails { get; set; }
        public DbSet<LeaveType> LeaveTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<LeaveApplication> leaveApplications { get; set; }
        public DbSet<SystemProfile> SystemProfiles { get; set; }
        public DbSet<Audit> Auditlogs { set; get; }
        public DbSet<RoleProfile> RoleProfiles { set; get; }
        public DbSet<Holiday> Holidays { set; get; }
        public DbSet<LeavePeriod> LeavePeriods { set; get; }
        public DbSet<LeaveAdjustmentEntry> LeaveAdjustmentEntries { set; get; }
        public DbSet<CompanyInformation> CompanyInformations { set; get; }
        public DbSet<ApprovalEntry> ApprovalEntries { set; get; }
        public DbSet<WorkFlowUserGroup> WorkFlowUserGroups { set; get; }
        public DbSet<WorkFlowUserGroupMember> WorkFlowUserGroupMembers { set; get; }
        public DbSet<ApprovalsUserMatrix> ApprovalsUserMatrices { set; get; }
        public virtual async Task<int> SaveChangesAsync(string UserId = null)
        {
            OnBeforeSavingChanges(UserId);
            var result = await base.SaveChangesAsync();
            return result;
        }
        private void OnBeforeSavingChanges(string UserId)
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<AuditEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;
                var auditEntry = new AuditEntry(entry);
                auditEntry.TableName = entry.Entity.GetType().Name;
                auditEntry.UserId = UserId;
                auditEntries.Add(auditEntry);

                foreach (var property in entry.Properties)
                {
                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.CurrentValue;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;

                            }
                            break;
                    }
                }
            }
            foreach (var auditentry in auditEntries)
            {
                Auditlogs.Add(auditentry.ToAudit());
            }
        }
    }
}