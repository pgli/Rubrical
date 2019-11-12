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
            Cell cell = new Cell { RowId = newRow.Id, Text = "newRubricCell" };
            newRow.Cells.Add(cell);
            rubric.Rows.Add(newRow);

            _applicationDbContext.Cells.Add(cell);
            _applicationDbContext.Rows.Add(newRow);
            _applicationDbContext.Rubrics.Add(rubric);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("Index", "Rubric");
        }

        public async Task<IActionResult> RubricView(int rubricId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var rubric = await _applicationDbContext.Rubrics.SingleOrDefaultAsync(x => x.Id == rubricId);
            if (rubric == null || (rubric.IsPrivate && rubric.ApplicationUserId != currentUser.Id))
            {
                return RedirectToAction("Index", "Home");
            }

            rubric.Rows = _applicationDbContext.Rows.Where(r => r.RubricId == rubric.Id).Include("Cells").ToList();
            if (rubric.ApplicationUserId == currentUser.Id)
            {
                ViewBag.IsOwner = true;
            }
            else
            {
                ViewBag.IsOwner = false;
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
    }
}