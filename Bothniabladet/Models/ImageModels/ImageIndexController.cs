using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Bothniabladet.Models.ImageModels
{
    public class ImageIndexController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}