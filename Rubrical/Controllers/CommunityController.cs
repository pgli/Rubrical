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
            var publicUserRubrics = await _applicationDbContext.Rubrics.Where(x => x.IsPrivate == false).Include(x => x.CreatedByUser).OrderByDescending(x => x.TotalRating).ToListAsync();
            var allSubjects = await _applicationDbContext.Subjects.OrderBy(x => x.SubjectName).ToListAsync();
            var allGrades = await _applicationDbContext.Grades.OrderBy(x => x.Number).ThenBy(x => x.GradeName).ToListAsync();

            var indexVM = new CommunityIndexViewModel { Rubrics = publicUserRubrics, Subjects = allSubjects, Grades = allGrades };

            return View(indexVM);
        }

        [HttpPost]
        public async Task<IActionResult> FilterRubrics([FromBody] CommunityIndexViewModel filterModel)
        {
            var publicUserRubrics = new List<Rubric>();
            List<CommunityFilteredViewModel> communityFilteredViewModel = null;

            if (filterModel.FilterGradeId > 0)
            {
                if (filterModel.FilterSubjectId > 0)
                {
                    // filter by both Grade and Subject
                    publicUserRubrics = await _applicationDbContext.Rubrics.Where(x => x.IsPrivate == false && x.GradeId == filterModel.FilterGradeId && x.SubjectId == filterModel.FilterSubjectId).Include(x => x.CreatedByUser).Include(x => x.Grade).Include(x => x.Subject).OrderByDescending(x => x.TotalRating).ToListAsync();
                }
                else
                {
                    // filter by Grade only
                    publicUserRubrics = await _applicationDbContext.Rubrics.Where(x => x.IsPrivate == false && x.GradeId == filterModel.FilterGradeId).Include(x => x.CreatedByUser).Include(x => x.Grade).Include(x => x.Subject).OrderByDescending(x => x.TotalRating).ToListAsync();
                }

                communityFilteredViewModel = new List<CommunityFilteredViewModel>();

                foreach (var rubric in publicUserRubrics)
                {
                    communityFilteredViewModel.Add(new CommunityFilteredViewModel
                    {
                        RubricId = rubric.Id,
                        UserName = rubric.CreatedByUser.UserName,
                        DateCreated = rubric.DateCreated,
                        Grade = rubric.Grade,
                        Subject = rubric.Subject,
                        Title = rubric.Title,
                        Description = rubric.Description,
                        TotalRating = rubric.TotalRating
                    });
                }

                return Json(sort(filterModel.SortType, communityFilteredViewModel));
            }
            else
            {
                if (filterModel.FilterSubjectId > 0)
                {
                    // filter by Subject only
                    publicUserRubrics = await _applicationDbContext.Rubrics.Where(x => x.IsPrivate == false && x.SubjectId == filterModel.FilterSubjectId).Include(x => x.CreatedByUser).Include(x => x.Grade).Include(x => x.Subject).OrderByDescending(x => x.TotalRating).ToListAsync();

                    communityFilteredViewModel = new List<CommunityFilteredViewModel>();

                    foreach (var rubric in publicUserRubrics)
                    {
                        communityFilteredViewModel.Add(new CommunityFilteredViewModel
                        {
                            RubricId = rubric.Id,
                            UserName = rubric.CreatedByUser.UserName,
                            DateCreated = rubric.DateCreated,
                            Grade = rubric.Grade,
                            Subject = rubric.Subject,
                            Title = rubric.Title,
                            Description = rubric.Description,
                            TotalRating = rubric.TotalRating
                        });
                    }

                    return Json(sort(filterModel.SortType, communityFilteredViewModel));
                }
            }

            publicUserRubrics = await _applicationDbContext.Rubrics.Where(x => x.IsPrivate == false).Include(x => x.CreatedByUser).Include(x => x.Grade).Include(x => x.Subject).OrderByDescending(x => x.TotalRating).ToListAsync();
            communityFilteredViewModel = new List<CommunityFilteredViewModel>();

            foreach (var rubric in publicUserRubrics)
            {
                communityFilteredViewModel.Add(new CommunityFilteredViewModel
                {
                    RubricId = rubric.Id,
                    UserName = rubric.CreatedByUser.UserName,
                    DateCreated = rubric.DateCreated,
                    Grade = rubric.Grade,
                    Subject = rubric.Subject,
                    Title = rubric.Title,
                    Description = rubric.Description,
                    TotalRating = rubric.TotalRating
                });
            }
            
            return Json(sort(filterModel.SortType, communityFilteredViewModel));
        }

        public List<CommunityFilteredViewModel> sort(int sortType, List<CommunityFilteredViewModel> data)
        {
            switch (sortType)
            {
                case 1:
                    return data.OrderBy(d => d.TotalRating).ToList();
                case 2:
                    return data.OrderBy(d => d.Title).ToList();
                case 3:
                    return data.OrderByDescending(d => d.Title).ToList();
                case 4:
                    return data.OrderByDescending(d => d.DateCreated).ToList();
                case 5:
                    return data.OrderBy(d => d.DateCreated).ToList();
                default:
                    return data.OrderByDescending(d => d.TotalRating).ToList();
            }
        }
    }
}