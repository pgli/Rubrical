using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rubrical.Data;
using Rubrical.Models;
using Rubrical.RubricModels;

namespace Rubrical.Controllers
{
    [Authorize]
    public class RubricController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private UserManager<ApplicationUser> _userManager;

        public RubricController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var currentUserRubrics = await _applicationDbContext.Rubrics.Where(x => x.ApplicationUserId == currentUser.Id).ToListAsync();

            var indexVM = new RubricIndexViewModel { Rubrics = currentUserRubrics };

            return View(indexVM);
        }

        public async Task<IActionResult> Create()
        {
            var subjects = await _applicationDbContext.Subjects.ToListAsync();
            var grades = await _applicationDbContext.Grades.ToListAsync();

            return View(new RubricViewModel { Subjects = subjects, Grades = grades });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRubric(CreateRubricModel createRubricModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var rubric = new Rubric
            {
                ApplicationUserId = currentUser.Id,
                DateCreated = DateTime.Now,
                Title = createRubricModel.Title,
                Description = createRubricModel.Description,
                GradeId = createRubricModel.Grade,
                SubjectId = createRubricModel.Subject,
                IsPrivate = createRubricModel.IsPrivate,
                TotalRating = 0
            };

            _applicationDbContext.Rubrics.Add(rubric);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Rubric");
        }

        public async Task<IActionResult> RubricView(int rubricId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var rubric = await _applicationDbContext.Rubrics.FirstOrDefaultAsync(x => x.Id == rubricId);
            if (rubric == null || (rubric.IsPrivate && rubric.ApplicationUserId != currentUser.Id))
            {
                return RedirectToAction("Index", "Home");
            }

            return View(rubric);
        }
    }
}