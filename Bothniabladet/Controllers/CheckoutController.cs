using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bothniabladet.Models.Checkout;
using Bothniabladet.Data;
using Microsoft.AspNetCore.Authorization;
using Bothniabladet.Services;
using Microsoft.AspNetCore.Identity;

namespace Bothniabladet.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {

        //possible to remove the AppDbContext once Services are fully implemented
        private readonly AppDbContext _context;
        public CheckoutService _service;
        public CheckoutController(AppDbContext context, CheckoutService service)
        {
            _context = context;
            _service = service;
        }

        // GET: /Checkout/AddressAndPayment
        public IActionResult AddressAndPayment()
        {
            return View(_service.GetShoppingCart()); // Shows item in the shoppingcart
        }

        // POST: /Checkout/AddressAndPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddressAndPayment(int id)
        {
            _service.AddItem(id);  // Adds the new item to the shoppingcart
            return View(_service.GetShoppingCart()); // Shows the new item in the shoppingcart
        }

        //Better name would be to prefer but not top priotity right now

        // GET: /Checkout/Complete
        public IActionResult Complete()
        {
            return View();
        }

        //POST: /Checkout/FinalForm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalForm()
        {
            _service.CompleteTransaction();
            return View();
        }

    }
}