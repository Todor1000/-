﻿using Microsoft.AspNetCore.Mvc;

namespace Локален_Бюлетински_Помошник.Controllers
{
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}