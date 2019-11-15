using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var currentUserRubrics = await _applicationDbContext.Rubrics.Where(x => x.ApplicationUserId == currentUser.Id).OrderByDescending(x => x.DateCreated).ToListAsync();

            var indexVM = new RubricIndexViewModel { Rubrics = currentUserRubrics };

            return View(indexVM);
        }

        public async Task<IActionResult> Create()
        {
            var subjects = await _applicationDbContext.Subjects.OrderBy(x => x.SubjectName).ToListAsync();
            var grades = await _applicationDbContext.Grades.OrderBy(x => x.Number).ThenBy(x => x.GradeName).ToListAsync();

            return View(new RubricViewModel { Subjects = subjects, Grades = grades });
        }

        [HttpPost]
        public async Task<IActionResult> CreateRubric(RubricCreateModel rubricCreateModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var rubric = new Rubric
            {
                ApplicationUserId = currentUser.Id,
                DateCreated = DateTime.Now,
                Title = rubricCreateModel.Title,
                Description = rubricCreateModel.Description,
                GradeId = rubricCreateModel.Grade,
                SubjectId = rubricCreateModel.Subject,
                IsPrivate = rubricCreateModel.IsPrivate,
                TotalRating = 0
            };
            var newRow = new Row { RubricId = rubric.Id };
            Cell cell = new Cell { RowId = newRow.Id, Text = "First Cell!" };
            newRow.Cells.Add(cell);
            rubric.Rows.Add(newRow);

            _applicationDbContext.Cells.Add(cell);
            _applicationDbContext.Rows.Add(newRow);
            _applicationDbContext.Rubrics.Add(rubric);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Rubric");
        }

        [AllowAnonymous]
        public async Task<IActionResult> RubricView(int rubricId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var rubric = await _applicationDbContext.Rubrics.Include(x => x.Ratings).SingleOrDefaultAsync(x => x.Id == rubricId);
            if (rubric == null || (rubric.IsPrivate && rubric.ApplicationUserId != currentUser.Id))
            {
                return RedirectToAction("Index", "Home");
            }

            rubric.Rows = _applicationDbContext.Rows.Where(r => r.RubricId == rubric.Id).Include("Cells").ToList();
            ViewBag.IsOwner = (currentUser?.Id == null) ? false : rubric.ApplicationUserId == currentUser.Id;

            if ((currentUser?.Id == null) ? false : rubric.Ratings.Any(x => x.ApplicationUserId == currentUser.Id))
            {
                var rating = rubric.Ratings.First(x => x.ApplicationUserId == currentUser.Id);
                ViewBag.Rating = rating.Value ? 1 : 0;
            }
            else
            {
                ViewBag.Rating = -1;
            }

            ViewBag.RubricId = rubric.Id;
            return View(rubric);
        }

        [HttpPost]
        public async Task<IActionResult> AddRow([FromBody] Row data)
        {
            var rubric = await _applicationDbContext.Rubrics.SingleOrDefaultAsync(r => r.Id == data.RubricId);
            if (rubric == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Rubric not found.");
            }

            var newRow = new Row { RubricId = rubric.Id };

            _applicationDbContext.Rows.Add(newRow);
            await _applicationDbContext.SaveChangesAsync();

            var cellsPerRow = _applicationDbContext.Rows.Include(c => c.Cells).First(x => x.RubricId == rubric.Id).Cells.Count();
            for (int i = 0; i < cellsPerRow; i++)
            {
                Cell cell = new Cell { RowId = newRow.Id, Text = "" };
                newRow.Cells.Add(cell);
                _applicationDbContext.Cells.Add(cell);
            }

            await _applicationDbContext.SaveChangesAsync();
            return Json(new RowViewModel { Id = newRow.Id, Cells = newRow.Cells });
        }

        [HttpPost]
        public async Task<IActionResult> AddColumn([FromBody] CellEditModel cellEditModel)
        {
            var rubric = await _applicationDbContext.Rubrics.Include(r => r.Rows).SingleOrDefaultAsync(x => x.Id == cellEditModel.RubricId);
            var currentUser = await _userManager.GetUserAsync(User);
            var isOwner = await _applicationDbContext.Rubrics.Where(x => x.ApplicationUserId == currentUser.Id && x.Id == cellEditModel.RubricId).AnyAsync();
            if (!isOwner)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Insufficient permissions.");
            }

            var cellIds = new List<int>();
            foreach (var row in rubric.Rows)
            {
                Cell cell = new Cell { RowId = row.Id, Text = "" };
                row.Cells.Add(cell);

                await _applicationDbContext.SaveChangesAsync();
                cellIds.Add(cell.Id);
            }

            return Json(cellIds);
        }

        [HttpPost]
        public async Task<IActionResult> EditCell([FromBody] CellEditModel cellEditModel)
        {
            var rubric = await _applicationDbContext.Rubrics.SingleOrDefaultAsync(r => r.Id == cellEditModel.RubricId);
            if (rubric == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Rubric not found.");
            }

            var cell = await _applicationDbContext.Cells.Include(r => r.Row).SingleOrDefaultAsync(c => c.Id == cellEditModel.CellId);
            if (cell == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Cell not found.");
            }

            var row = await _applicationDbContext.Rows.Include(r => r.Rubric).SingleOrDefaultAsync(r => r.Id == cell.RowId);
            if (row == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Row not found.");
            }

            var currentUser = await _userManager.GetUserAsync(User);
            var isOwner = await _applicationDbContext.Rubrics.Where(x => x.ApplicationUserId == currentUser.Id && x.Id == row.RubricId).AnyAsync();

            if (!isOwner)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Insufficient permissions.");
            }

            cell.Text = cellEditModel.Text;

            await _applicationDbContext.SaveChangesAsync();
            return Json("Successfully edited.");
        }

        [HttpPost]
        public async Task<IActionResult> EditPrivacy([FromBody] PrivacyEditModel privacyEditModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var isOwner = await _applicationDbContext.Rubrics.Where(x => x.ApplicationUserId == currentUser.Id && x.Id == privacyEditModel.RubricId).AnyAsync();

            if (!isOwner)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Insufficient permissions.");
            }

            var rubric = await _applicationDbContext.Rubrics.SingleOrDefaultAsync(r => r.Id == privacyEditModel.RubricId);
            if (rubric == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Rubric not found.");
            }

            if (privacyEditModel.SelectedPrivacy == 0)
            {
                rubric.IsPrivate = false;
                await _applicationDbContext.SaveChangesAsync();
                return Json("Successfully changed privacy.");
            }
            else
            {
                rubric.IsPrivate = true;
                await _applicationDbContext.SaveChangesAsync();
                return Json("Successfully changed privacy.");
            }
        }

        [HttpPost]
        public async Task<IActionResult> EditRating([FromBody] SetRatingModel setRatingModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var rubric = await _applicationDbContext.Rubrics.Include(x => x.Ratings)
                                                            .SingleOrDefaultAsync(x => x.Id == setRatingModel.RubricId);
            if (rubric == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Rubric not found.");
            }

            if (rubric.Ratings.Where(x => x.ApplicationUserId == currentUser.Id).Any())
            {
                var rating = rubric.Ratings.First(x => x.ApplicationUserId == currentUser.Id);
                if (rating.Value == setRatingModel.Value)
                {
                    //user undoes vote
                    rubric.Ratings.Remove(rating);
                    rubric.TotalRating = setRatingModel.Value ? rubric.TotalRating - 1 : rubric.TotalRating + 1;
                }
                else
                {
                    //user switches directly from one vote to another
                    rating.Value = setRatingModel.Value;
                    rubric.TotalRating = setRatingModel.Value ? rubric.TotalRating + 2 : rubric.TotalRating - 2;
                }

            }
            else
            {
                await _applicationDbContext.Ratings.AddAsync(new Rating
                {
                    ApplicationUserId = currentUser.Id,
                    RubricId = setRatingModel.RubricId,
                    Value = setRatingModel.Value
                });

                rubric.TotalRating = setRatingModel.Value ? rubric.TotalRating + 1 : rubric.TotalRating - 1;
            }

            await _applicationDbContext.SaveChangesAsync();

            var ratingValue = -1;
            if (rubric.Ratings.Any(x => x.ApplicationUserId == currentUser.Id))
            {
                var rating = rubric.Ratings.First(x => x.ApplicationUserId == currentUser.Id);
                ratingValue = rating.Value ? 1 : 0;
            }
            return Json(new RatingViewModel
            {
                Vote = ratingValue,
                TotalRating = rubric.TotalRating.HasValue ? rubric.TotalRating.Value : 0
            });
        }

        [HttpPost]
        public async Task<IActionResult> DeleteRubric([FromBody] RubricDeleteViewModel rubricDeleteViewModel)
        {
            var adminRole = await _applicationDbContext.Roles.SingleOrDefaultAsync(r => r.Name.Equals("Admin"));
            var adminRoleId = adminRole.Id;
            var currentUser = await _userManager.GetUserAsync(User);
            var isOwner = await _applicationDbContext.Rubrics.Where(x => x.ApplicationUserId == currentUser.Id && x.Id == rubricDeleteViewModel.RubricId).AnyAsync();
            var isAdmin = await _applicationDbContext.UserRoles.AnyAsync(y => y.UserId.Equals(currentUser.Id) && y.RoleId.Equals(adminRoleId));

            if (!isOwner && !isAdmin)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Insufficient permissions.");
            }

            var rubric = await _applicationDbContext.Rubrics.SingleOrDefaultAsync(r => r.Id == rubricDeleteViewModel.RubricId);
            if (rubric == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Rubric not found.");
            }

            _applicationDbContext.Rubrics.Remove(rubric);
            await _applicationDbContext.SaveChangesAsync();

            return Json("Successfully deleted rubric.");
        }


        [HttpPost]
        public async Task<IActionResult> CopyRubric([FromBody] RubricCreateModel rubricCreateModel)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var rubric = await _applicationDbContext.Rubrics.Include(r => r.Rows).SingleOrDefaultAsync(r => r.Id == rubricCreateModel.RubricId);
            if (rubric == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Rubric not found.");
            }

            var newRubric = new Rubric
            {
                Title = rubricCreateModel.Title,
                ApplicationUserId = currentUser.Id,
                Description = rubric.Description,
                SubjectId = rubric.SubjectId,
                GradeId = rubric.GradeId,
                TotalRating = 0,
                IsPrivate = true,
                DateCreated = DateTime.Now
            };
            _applicationDbContext.Rubrics.Add(newRubric);
            await _applicationDbContext.SaveChangesAsync();

            var newRows = new List<Row>();
            foreach (var oldRow in rubric.Rows)
            {
                var newRow = new Row
                {
                    RubricId = newRubric.Id
                };
                _applicationDbContext.Rows.Add(newRow);
                await _applicationDbContext.SaveChangesAsync();

                var oldCells = await _applicationDbContext.Cells.Where(x => x.RowId == oldRow.Id).ToListAsync();
                foreach (var oldCell in oldCells)
                {
                    newRow.Cells.Add(new Cell
                    {
                        RowId = newRow.Id,
                        Text = oldCell.Text
                    });
                }

                await _applicationDbContext.SaveChangesAsync();
            }

            return Json(newRubric.Id);
        }
    }
}