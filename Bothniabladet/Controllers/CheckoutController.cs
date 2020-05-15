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

        // GET: /Checkout/AddressAndPayment (For now, need to remove the last checkout)
        public IActionResult AddressAndPayment()
        {
            return View();
        }

        // Add item to modal shoppingcart and repopulate the modal too show all items
        [HttpPost]
        public IActionResult AddItem(int id)
        {
            var shoppingCartModel = _service.GetShoppingCart();
            
            if (shoppingCartModel == null) //If the shopping cart is empty create new
            {
                _service.NewShoppingCart();
            }
            
            if(id != 0)
            {
                _service.AddItem(id);
            }

            shoppingCartModel = _service.GetShoppingCart();
            return View(shoppingCartModel);
        }

        // GET: /Checkout/Complete
        public IActionResult Complete()
        {
            return View();
        }
    }
}