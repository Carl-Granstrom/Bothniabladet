using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Bothniabladet.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bothniabladet.Controllers
{
  [Authorize(Roles = "Admin")]
  public class AdminController : Controller
  {
    
    public IActionResult startAdmin()
    {
      ViewBag.Title = "Admin Page";
      return View();
    }
  }
}
