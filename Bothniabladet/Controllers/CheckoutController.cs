using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bothniabladet.Models.Checkout;

namespace Bothniabladet.Controllers
{
    public class CheckoutController : Controller
    {
        public IActionResult Checkout(CheckoutViewModel checkout)
        {
            return View(checkout);
        }
    }
}