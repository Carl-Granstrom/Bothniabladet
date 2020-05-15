using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bothniabladet.Models.Checkout;
using Bothniabladet.Data;
using Microsoft.AspNetCore.Authorization;

namespace Bothniabladet.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {

        // GET: /Checkout/AddressAndPayment (For now, need to remove the last checkout)

        public IActionResult AddressAndPayment()
        {
            return View();
        }

        // Add item to modal and repopulate the modal too show all items
        public IActionResult AddItem()
        {
            return View();
        }

        // GET: /Checkout/Complete
        public IActionResult Complete()
        {
            return View();
        }
    }
}