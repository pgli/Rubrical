using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rubrical.AdminModels;
using Rubrical.Data;
using Rubrical.Models;

namespace Rubrical.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var adminRoleId = (await _applicationDbContext.Roles.SingleOrDefaultAsync(r => r.Name.Equals("Admin"))).Id;
            var users = _applicationDbContext.ApplicationUsers.Select(x => new ApplicationUserViewModel
            {
                Id = x.Id,
                IsAdmin = _applicationDbContext.UserRoles.Any(y => y.UserId.Equals(x.Id) && y.RoleId.Equals(adminRoleId)),
                Name = x.UserName
            });

            return View(new ApplicationUserListViewModel
            {
                Users = await users.ToListAsync()
            });
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRole([FromBody] ApplicationUserViewModel applicationUserViewModel)
        {
            var id = applicationUserViewModel.Id;
            var user = await _applicationDbContext.ApplicationUsers.SingleOrDefaultAsync(u => u.Id.Equals(id));
            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("No user found");
            }

            var adminRoleId = (await _applicationDbContext.Roles.SingleOrDefaultAsync(r => r.Name.Equals("Admin"))).Id;

            var isAlreadyAdmin = await _applicationDbContext.UserRoles.AnyAsync(r => r.UserId.Equals(user.Id) && r.RoleId.Equals(adminRoleId));
            if (isAlreadyAdmin)
            {
                _applicationDbContext.UserRoles.RemoveRange(_applicationDbContext.UserRoles.Where(r => r.UserId.Equals(user.Id) && r.RoleId.Equals(adminRoleId)));
            }
            else
            {
                await _applicationDbContext.UserRoles.AddAsync(new IdentityUserRole<string>
                {
                    UserId = user.Id,
                    RoleId = adminRoleId
                });
            }

            await _applicationDbContext.SaveChangesAsync();
            return Json("Updated role.");
        }

        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromBody] ApplicationUserViewModel applicationUserViewModel)
        {
            var id = applicationUserViewModel.Id;
            var user = await _applicationDbContext.ApplicationUsers.SingleOrDefaultAsync(u => u.Id.Equals(id));
            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("No user found");
            }

            _applicationDbContext.ApplicationUsers.Remove(user);

            await _applicationDbContext.SaveChangesAsync();
            return Json("Deleted user.");
        }
    }
}