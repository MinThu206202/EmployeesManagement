using System.Security.Claims;
using EmployeesManagement.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeesManagement.ViewModels;

namespace EmployeesManagement.Models
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProfilesController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tasks = new ProfileViewModel();
            var roles = await _context.Roles.OrderBy(x => x.Name).ToListAsync();
            ViewBag.Roles = new SelectList(roles, "Id", "Name");

            var systemtasks = await _context.SystemProfiles
                                            .Include("Children.Children.Children")
                                            .OrderBy(x => x.Order)
                                            .ToListAsync();

            ViewBag.Tasks = new SelectList(systemtasks, "Id", "Name");

            return View(tasks);
        }

        public async Task<ActionResult> AssignRights(ProfileViewModel vm)
        {
            var Userid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var role = new RoleProfile
            {
                TaskId = vm.TaskId,
                RoleId = vm.RoleId,
            };
            _context.RoleProfiles.Add(role);
            await _context.SaveChangesAsync(Userid);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UserRights(string id)
        {
            var tasks = new ProfileViewModel();
            tasks.RoleId = id;

            var Profiles = await _context.SystemProfiles
                .Include(s => s.Profile)
                .Include("Children.Children.Children")
                .OrderBy(x => x.Order)
                .ToListAsync();

            // ✅ THIS LINE IS MISSING
            tasks.Profiles = Profiles;

            tasks.RolesRightsIds = await _context.RoleProfiles
                .Where(x => x.RoleId == id)
                .Select(r => r.TaskId)
                .ToListAsync();

            return View(tasks);
        }

        [HttpPost]
        public async Task<ActionResult> UserGroupRights(string id, ProfileViewModel vm)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var allrights = await _context.RoleProfiles.Where(x => x.RoleId == id).ToListAsync();

            _context.RoleProfiles.RemoveRange(allrights);
            await _context.SaveChangesAsync(userId);


            foreach (var taskId in vm.Ids)
            {
                var role = new RoleProfile
                {
                    TaskId = taskId, // ✅ FIXED
                    RoleId = vm.RoleId,
                };

                _context.RoleProfiles.Add(role);
                await _context.SaveChangesAsync(userId);

            }


            return RedirectToAction("UserRights", new { id = vm.RoleId });
        }
    }
}