using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var publicUserRubrics = await _applicationDbContext.Rubrics.Where(x => x.IsPrivate == false).Include(x => x.CreatedByUser).OrderBy(x => x.TotalRating).ToListAsync();
            var allSubjects = await _applicationDbContext.Subjects.OrderBy(x => x.SubjectName).ToListAsync();
            var allGrades = await _applicationDbContext.Grades.OrderBy(x => x.Number).ThenBy(x => x.GradeName).ToListAsync();

            var indexVM = new CommunityIndexViewModel { Rubrics = publicUserRubrics, Subjects = allSubjects, Grades = allGrades };

            return View(indexVM);
        }

        [HttpPost]
        public async Task<IActionResult> FilterByGrade([FromBody] CommunityIndexViewModel filterModel)
        {
            if (filterModel.FilterGradeId > 0)
            {
                var publicUserRubrics = await _applicationDbContext.Rubrics.Where(x => x.IsPrivate == false && x.GradeId == filterModel.FilterGradeId).Include(x => x.CreatedByUser).Include(x => x.Grade).Include(x => x.Subject).OrderBy(x => x.TotalRating).ToListAsync();
                var indexVM = new CommunityIndexViewModel { Rubrics = publicUserRubrics, Subjects = filterModel.Subjects, Grades = filterModel.Grades, FilterGradeId = filterModel.FilterGradeId, FilterSubjectId = filterModel.FilterSubjectId };

                var json = Json(indexVM);
                return Json(indexVM);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return Json("Error filtering grade.");
        }
    }
}