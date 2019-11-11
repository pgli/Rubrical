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
            var newRow = new Row { Name = "newRubricRow", RubricId = rubric.Id };
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
            var rubric = await _applicationDbContext.Rubrics.FirstOrDefaultAsync(x => x.Id == rubricId);
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


            return View(rubric);
        }

        public async Task<IActionResult> AddRow(Rubric rubric)
        {
            var newRow = new Row { Name = "temp", RubricId = rubric.Id };
            newRow.Cells = new List<Cell>();
            //var cellsPerRow = _applicationDbContext.Rows.Where(x => x.RubricId == rubric.Id).Include(c => c.Cells).FirstOrDefault().Cells.Count();
            var cellsPerRow = _applicationDbContext.Rows.Include(c => c.Cells).First(x => x.RubricId == rubric.Id).Cells.Count();

            for (int i = 0; i < cellsPerRow; i++)
            {
                Cell cell = new Cell { RowId = newRow.Id, Text = $"row{rubric.Rows.Count()}" };
                newRow.Cells.Add(cell);
                _applicationDbContext.Cells.Add(cell);
            }

            _applicationDbContext.Rows.Add(newRow);
            rubric.Rows.Add(newRow);
            Rubric rubricToUpdate = _applicationDbContext.Rubrics.Where(r => r.Id == rubric.Id).FirstOrDefault();
            if (rubricToUpdate != null)
            {
                _applicationDbContext.Rubrics.Update(rubricToUpdate);
            }

            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("RubricView", new { rubricId = rubric.Id });
        }

        public async Task<IActionResult> AddColumn(Rubric rubric)
        {
            // goal is to add a blank cell to the end of all currently existing rows
            // get all rows where rubric id matches
            // add to Rows list
            var rows = _applicationDbContext.Rows.Where(r => r.RubricId == rubric.Id).ToList();
            var rowsPerRubric = _applicationDbContext.Rows.Where(r => r.RubricId == rubric.Id).Count(); // amount we need to loop through

            // iterate over each row, adding new Cell object to the end
            // for each original row, change then .Update
            foreach (var row in rows)
            {
                Cell cell = new Cell { RowId = row.Id, Text = $"col{row.Cells.Count}" };
                row.Cells.Add(cell);
                _applicationDbContext.Cells.Add(cell);
                _applicationDbContext.Rows.Update(row);
            }

            // rubric.Rows = newRows
            // update rubric
            // save db context
            rubric.Rows = rows;
            _applicationDbContext.Rubrics.Update(rubric);
            await _applicationDbContext.SaveChangesAsync();

            return RedirectToAction("RubricView", new { rubricId = rubric.Id });
        }

        [HttpPost]
        public void SaveChanges([FromBody] string rows)
        {
            var json = rows;
            return;
        }
    }
}