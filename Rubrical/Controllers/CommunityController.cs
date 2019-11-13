using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rubrical.CommunityModels;
using Rubrical.Data;
using Rubrical.Models;
using Rubrical.RubricModels;

namespace Rubrical.Controllers
{
    public class CommunityController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private UserManager<ApplicationUser> _userManager;

        public CommunityController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var publicUserRubrics = await _applicationDbContext.Rubrics.Where(x => x.IsPrivate == false).ToListAsync();
            var allSubjects = await _applicationDbContext.Subjects.OrderBy(s => s.SubjectName).ToListAsync();
            var allGrades = await _applicationDbContext.Grades.OrderBy(g => g.GradeName).ToListAsync();

            var indexVM = new CommunityIndexViewModel { Rubrics = publicUserRubrics, Subjects = allSubjects, Grades = allGrades };

            return View(indexVM);
        }
    }
}