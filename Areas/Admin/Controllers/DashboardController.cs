﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExamBilet2.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        [Area("Admin")]
        [Authorize(Roles ="Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
