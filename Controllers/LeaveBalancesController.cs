using Microsoft.AspNetCore.Mvc;
using EmployeesManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesManagement.Models;
using System.Security.Claims;


namespace EmployeesManagement.Controllers
{
    public class LeaveBalancesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;

        public LeaveBalancesController(IConfiguration configuration, ApplicationDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var result = await _context.Employees
                                      .Include(x => x.Status)
                                      .ToListAsync();

            return View(result);
        }

        [HttpGet]
        public IActionResult AdjustLeaveBalance(int id)
        {
            LeaveAdjustmentEntry leaveAdjustment = new();
            leaveAdjustment.EmployeeId = id;
            ViewData["AdjustmentTypeId"] = new SelectList(_context.SystemCodeDetails
                                        .Include(y => y.SystemCode)
                                        .Where(x => x.SystemCode.Code == "LeaveAdjustment"), "Id", "Description");
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", id);

            ViewData["LeavePeriodId"] = new SelectList(_context.LeavePeriods.Where(x => x.Closed == false), "Id", "Name");

            return View(leaveAdjustment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdjustLeaveBalance(LeaveAdjustmentEntry leaveAdjustmentEntry)
        {
            if (ModelState.IsValid)
            {
                var adjustmenttype = await _context.SystemCodeDetails
                                .Include(x => x.SystemCode)
                                .Where(x => x.Id == leaveAdjustmentEntry.AdjustmentTypeId && x.SystemCode.Code == "LeaveAdjustment")
                            .FirstOrDefaultAsync();

                leaveAdjustmentEntry.AdjustmentDescription = leaveAdjustmentEntry.AdjustmentDescription + "-" + adjustmenttype.Description;
                leaveAdjustmentEntry.Id = 0;
                var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);

                _context.Add(leaveAdjustmentEntry);
                await _context.SaveChangesAsync(Userid);

                var employee = await _context.Employees.FindAsync(leaveAdjustmentEntry.EmployeeId);
                if (adjustmenttype.Code == "Positive")
                {
                    employee.LeaveOutStandingBalance = (employee.AllocatedLeaveDays - leaveAdjustmentEntry.NoOfDays);
                }
                else
                {
                    employee.LeaveOutStandingBalance = (employee.AllocatedLeaveDays - leaveAdjustmentEntry.NoOfDays);
                }
                _context.Update(employee);
                await _context.SaveChangesAsync(Userid);
            }

            return RedirectToAction(nameof(Index));
            ViewData["LeavePeriodId"] = new SelectList(_context.LeavePeriods.Where(x => x.Closed == false), "Id", "Name", leaveAdjustmentEntry.LeavePeriodId);
            ViewData["AdjustmentTypeId"] = new SelectList(_context.SystemCodeDetails, "Id", "Description", leaveAdjustmentEntry.AdjustmentTypeId);
            ViewData["EmployeeId"] = new SelectList(_context.Employees, "Id", "FullName", leaveAdjustmentEntry.EmployeeId);
            return View(leaveAdjustmentEntry);
        }
    }
}