using ExamBilet2.DbContextFile;
using ExamBilet2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ExamBilet2.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PositionController : Controller
    {
        private readonly AppDbContext _context;

        public PositionController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Position> positions = _context.Positions.ToList();
            return View(positions);
        }
        public IActionResult Create()
        {
            return View();
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Create(Position position)
        {
            if (!ModelState.IsValid) return View(position);

            _context.Positions.Add(position);
            _context.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Update(int id)
        {
            Position position = _context.Positions.FirstOrDefault(x => x.Id == id);
            if (position == null) return NotFound();
            return View(position);
        }
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult Update(Position position)
        {
            if (!ModelState.IsValid) return View(position);
            Position existposition = _context.Positions.FirstOrDefault(x => x.Id == position.Id);
            if (existposition == null) return NotFound();

            existposition.Name = position.Name;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {
            Position existposition = _context.Positions.FirstOrDefault(x => x.Id == id);
            if (existposition == null) return NotFound();
            _context.Positions.Remove(existposition);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
