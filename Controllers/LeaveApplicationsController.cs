using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesManagement.Data;
using EmployeesManagement.Models;

namespace EmployeesManagement.Controllers
{
    public class LeaveApplicationsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LeaveApplicationsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LeaveApplications
        public async Task<IActionResult> Index()
        {
            var approvedstatus = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.Code == "AwaitingApproval" && x.SystemCode.Code == "LeaveApprovalStatus")
                .FirstOrDefaultAsync();

            var applicationDbContext = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .ToListAsync();

            return View(applicationDbContext);
        }

        public async Task<IActionResult> ApproveLeaveApplications()
        {
            var approvedstatus = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.Code == "Approval" && x.SystemCode.Code == "LeaveApprovalStatus")
                .FirstOrDefaultAsync();

            var applicationDbContext = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == approvedstatus!.Id)
                .ToListAsync();

            return View(applicationDbContext);
        }


        public async Task<IActionResult> RejectLeaveApplications()
        {
            var approvedstatus = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.Code == "Reject" && x.SystemCode.Code == "LeaveApprovalStatus")
                .FirstOrDefaultAsync();

            var applicationDbContext = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .Where(l => l.StatusId == approvedstatus!.Id)
                .ToListAsync();

            return View(applicationDbContext);
        }

        // GET: LeaveApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var leaveApplication = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (leaveApplication == null) return NotFound();

            return View(leaveApplication);
        }

        // GET: Approve Leave
        [HttpGet]
        public async Task<IActionResult> ApproveLeave(int? id)
        {
            if (id == null) return NotFound();

            var leaveApplication = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (leaveApplication == null) return NotFound();

            return View(leaveApplication);
        }

        // POST: Approve Leave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ApproveLeave(LeaveApplication leave)
        {
            var approvedStatus = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.Code == "Approval" && x.SystemCode.Code == "LeaveApprovalStatus")
                .FirstOrDefaultAsync();

            var leaveApplication = await _context.leaveApplications
                .FirstOrDefaultAsync(m => m.Id == leave.Id);

            if (leaveApplication == null)
                return NotFound();

            leaveApplication.StatusId = approvedStatus.Id;
            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.ApprovedById = "Marco Code";
            leaveApplication.ApprovalNotes = leave.ApprovalNotes;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RejectLeave(LeaveApplication leave)
        {
            var approvedStatus = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(x => x.Code == "Reject" && x.SystemCode.Code == "LeaveApprovalStatus")
                .FirstOrDefaultAsync();

            var leaveApplication = await _context.leaveApplications
                .FirstOrDefaultAsync(m => m.Id == leave.Id);

            if (leaveApplication == null)
                return NotFound();

            leaveApplication.StatusId = approvedStatus.Id;
            leaveApplication.ApprovedOn = DateTime.Now;
            leaveApplication.ApprovedById = "Marco Code";
            leaveApplication.ApprovalNotes = leave.ApprovalNotes;
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: LeaveApplications/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdowns();
            return View();
        }

        // POST: LeaveApplications/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LeaveApplication leaveApplication)
        {
            if (ModelState.IsValid)
            {
                var pendingStatus = await _context.SystemCodeDetails
                    .Include(x => x.SystemCode)
                    .Where(y => y.Code == "AwaitingApproval" && y.SystemCode.Code == "LeaveApprovalStatus")
                    .FirstOrDefaultAsync();

                leaveApplication.CreatedOn = DateTime.Now;
                leaveApplication.CreatedById = "Marco Code";
                leaveApplication.StatusId = pendingStatus?.Id ?? 0;

                _context.Add(leaveApplication);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            await LoadDropdowns();
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var leaveApplication = await _context.leaveApplications.FindAsync(id);
            if (leaveApplication == null) return NotFound();

            await LoadDropdowns();
            return View(leaveApplication);
        }

        // POST: LeaveApplications/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LeaveApplication leaveApplication)
        {
            if (id != leaveApplication.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    leaveApplication.ModifiedOn = DateTime.Now;
                    leaveApplication.ModifiedById = "Marco Code";

                    _context.Update(leaveApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LeaveApplicationExists(leaveApplication.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            await LoadDropdowns();
            return View(leaveApplication);
        }

        // GET: LeaveApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var leaveApplication = await _context.leaveApplications
                .Include(l => l.Duration)
                .Include(l => l.Employee)
                .Include(l => l.LeaveType)
                .Include(l => l.Status)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (leaveApplication == null) return NotFound();

            return View(leaveApplication);
        }

        // POST: LeaveApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var leaveApplication = await _context.leaveApplications.FindAsync(id);

            if (leaveApplication != null)
            {
                _context.leaveApplications.Remove(leaveApplication);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool LeaveApplicationExists(int id)
        {
            return _context.leaveApplications.Any(e => e.Id == id);
        }

        // Dropdown Loader (Reusable)
        private async Task LoadDropdowns()
        {
            var durations = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveDuration")
                .ToListAsync();

            var employees = await _context.Employees.ToListAsync();
            var leaveTypes = await _context.LeaveTypes.ToListAsync();

            var statuses = await _context.SystemCodeDetails
                .Include(x => x.SystemCode)
                .Where(y => y.SystemCode.Code == "LeaveApprovalStatus")
                .ToListAsync();

            ViewData["DurationId"] = new SelectList(durations, "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(employees, "Id", "FullName");
            ViewData["LeaveTypeId"] = new SelectList(leaveTypes, "Id", "Name");
            ViewData["StatusId"] = new SelectList(statuses, "Id", "Description");
        }
    }
}