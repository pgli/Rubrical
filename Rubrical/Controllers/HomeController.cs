using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Rubrical.Data;
using Rubrical.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Rubrical.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager) : base()
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var userContent = _applicationDbContext.Users.Include(u => u.Rubrics).Where(u => u.Id == user.Id).First();
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}