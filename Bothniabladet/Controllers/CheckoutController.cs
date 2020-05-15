using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bothniabladet.Models.Checkout;
using Bothniabladet.Services;
using Bothniabladet.Data;
using Microsoft.AspNetCore.Authorization;

namespace Bothniabladet.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {

        // GET: /Checkout/Checkout (For now, need to remove the last checkout)
        public IActionResult Checkout()
        {
            return View();
        }

        // Add item to modal and repopulate the modal too show all items
        public IActionResult AddItem()
        {
            return View();
        }

        // After purchase add all items to the user and return to main page
        public IActionResult Purchase()
        {
            return View();
        }
    }
}