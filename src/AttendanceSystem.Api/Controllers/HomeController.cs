﻿using Microsoft.AspNetCore.Mvc;

namespace AttendanceSystem.Api.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
