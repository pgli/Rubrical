// ***********************************************************************
// Assembly         : Rubrical
// Author           : Admin
// Created          : 11-15-2019
//
// Last Modified By : Admin
// Last Modified On : 11-15-2019
// ***********************************************************************
// <copyright file="AdminController.cs" company="Rubrical">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
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
    /// <summary>
    /// Class AdminController.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Controller" />
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// The application database context
        /// </summary>
        private ApplicationDbContext _applicationDbContext;
        /// <summary>
        /// The user manager
        /// </summary>
        private UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminController"/> class.
        /// </summary>
        /// <param name="applicationDbContext">The application database context.</param>
        /// <param name="userManager">The user manager.</param>
        public AdminController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
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
                Users = await users.ToListAsync(),
                Subjects = await _applicationDbContext.Subjects.ToListAsync()
            });
        }

        /// <summary>
        /// Updates the role.
        /// </summary>
        /// <param name="applicationUserViewModel">The application user view model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
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

        /// <summary>
        /// Deletes the user.
        /// </summary>
        /// <param name="applicationUserViewModel">The application user view model.</param>
        /// <returns>Task&lt;IActionResult&gt;.</returns>
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