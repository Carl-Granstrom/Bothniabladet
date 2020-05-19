using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Bothniabladet.Controllers;
using Bothniabladet.Data;
using Bothniabladet.Models.Checkout;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Bothniabladet.Services;
using Bothniabladet.Models.ImageModels;

namespace Bothniabladet.Services
{
    public class CheckoutService
    {
        //Fetch the current user
        private IHttpContextAccessor _httpContextAccessor;
        AppDbContext _context;
        readonly ILogger _logger;
        //constructor
        public CheckoutService(AppDbContext context, ILoggerFactory factory, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _logger = factory.CreateLogger<ImageService>();
            _httpContextAccessor = httpContextAccessor;
        }

        // Loads active users shoppingcart
        public ShoppingCartModel GetShoppingCart()
        {
            // Query images linked to account
            List<ShoppingCart> shoppingCartIndex = _context.ShoppingCart
                .Where(shoppingCart => shoppingCart.UserId == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Where(shoppingCart => !shoppingCart.Owns)
                .Include(shoppingCart => shoppingCart.Image)
                .ToList();

            // Add imaegs to temporary holder
            List<Image> imageIndex = new List<Image>();
            foreach (ShoppingCart p in shoppingCartIndex)
            {
                imageIndex.Add(p.Image);
            }

            // Query accountinformation
            ShoppingCartModel shoppingCartModel = _context.ShoppingCart
                .Where(shoppingCart => shoppingCart.UserId == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Where(shoppingCart => !shoppingCart.Owns)
                .Select(shoppingCart => new ShoppingCartModel
                {
                    User = shoppingCart.User
                }).SingleOrDefault();

            shoppingCartModel.Images = imageIndex;

            return shoppingCartModel;
        }
        //Adds a item to the shoppingcart, if owned true item does not exist in shoppingcart
        public void AddItem(int id)
        {
            Image image = _context.Images.Find(id);
            ApplicationUser user = _context.Users.Find(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var newShoppingCart = new ShoppingCart();

            newShoppingCart.Image = image;
            newShoppingCart.User = user;
            newShoppingCart.Owns = false;
            _context.Add(newShoppingCart);
            _context.SaveChanges();
        }
    }
}
