using ExamBilet2.DbContextFile;
using ExamBilet2.Models;
using Microsoft.AspNetCore.Mvc;

namespace ExamBilet2.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Team> teams = _context.Teams.ToList();
            
            return View(teams);
        }

    
    }
}