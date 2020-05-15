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

        // GET: /Checkout/AddressAndPayment
        public IActionResult AddressAndPayment()
        {
            return View();
        }

        // Add item to modal and show
        public IActionResult AddItem()
        {
            return View();
        }


        public IActionResult Checkout()
        {
            return View();
        }

    }
}