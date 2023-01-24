using ExamBilet2.DbContextFile;
using ExamBilet2.Helpers;
using ExamBilet2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ExamBilet2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            List<Team> teams = _context.Teams.Include(x=>x.Position).ToList();
            return View(teams);
        }
        public IActionResult Create()
        {
            ViewBag.Positions = _context.Positions.ToList();
            return View();
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Create(Team team)
        {
            ViewBag.Positions = _context.Positions.ToList();
            if (!ModelState.IsValid) return View(team);

            if(team.ImageFile == null)
            {
                ModelState.AddModelError("ImageFile", "Required");
                return View(team);
            }

            if(! team.ImageFile.CheckFileLength(1048576*3))
            {
                ModelState.AddModelError("ImageFile", "Please, upload less than 3 Mb");
                return View(team);
            }
            if (!team.ImageFile.CheckFileType())
            {
                ModelState.AddModelError("ImageFile", "Please, upload only png,jpg,jpeg files");
                return View(team);
            }

            team.ImageUrl = team.ImageFile.SaveFile(_env.WebRootPath, "uploads/teams");

            _context.Teams.Add(team);
            _context.SaveChanges();

            return RedirectToAction("index", "team");
        }
        public IActionResult Update(int id)
        {
            ViewBag.Positions = _context.Positions.ToList();
            Team team = _context.Teams.Include(x=>x.Position).FirstOrDefault(t => t.Id == id);
            ViewBag.Image = team.ImageUrl;
            if (team == null) return NotFound();
            return View(team);
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Update(Team team)
        {
            Team existteam = _context.Teams.Include(x => x.Position).FirstOrDefault(t => t.Id == team.Id);
            ViewBag.Image = existteam.ImageUrl;
            ViewBag.Positions = _context.Positions.ToList();
            if (!ModelState.IsValid) return View(team);

            if (team.ImageFile != null)
            {
                if (!team.ImageFile.CheckFileLength(1048576 * 3))
                {
                    ModelState.AddModelError("ImageFile", "Please, upload less than 3 Mb");
                    return View(team);
                }
                if (!team.ImageFile.CheckFileType())
                {
                    ModelState.AddModelError("ImageFile", "Please, upload only png,jpg,jpeg files");
                    return View(team);
                }
                team.ImageUrl = team.ImageFile.SaveFile(_env.WebRootPath, "uploads/teams");

                string path = Path.Combine(_env.WebRootPath, "uploads/teams", existteam.ImageUrl);
                if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

                existteam.ImageUrl = team.ImageUrl;
            }

            existteam.PositionId = team.PositionId;
            existteam.TwitterUrl = team?.TwitterUrl;
            existteam.InstaUrl = team?.InstaUrl;
            existteam.LnUrl = team?.LnUrl;
            existteam.FbUrl = team?.FbUrl;
            existteam.FullName = team.FullName;


            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Team existteam = _context.Teams.FirstOrDefault(x => x.Id == id);
            if (existteam == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "uploads/teams", existteam.ImageUrl);
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);

            _context.Teams.Remove(existteam);
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }
    }
}
