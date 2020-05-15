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
            ShoppingCartModel shoppingCartModel = _context.ShoppingCart
                .Where(shoppingCart => shoppingCart.UserId == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Where(shoppingCart => !shoppingCart.Completed)
                .Select(shoppingCart => new ShoppingCartModel
                {
                    CartId = shoppingCart.CartId,
                    Images = shoppingCart.Images,
                    Name = shoppingCart.Name,
                    Email = shoppingCart.Email

                }).SingleOrDefault();

            return shoppingCartModel;
        }

        // Adds a new shoppingcart to a user
        public void NewShoppingCart()
        {

            var newShoppingCart = new ShoppingCart();

            newShoppingCart.UserId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            newShoppingCart.Name = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            newShoppingCart.Email = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Email);
            newShoppingCart.Completed = false;
            _context.Add(newShoppingCart);
            _context.SaveChanges();

        }

        public void AddItem(int id)
        {
            ShoppingCartModel shoppingCartModel = GetShoppingCart();
            shoppingCartModel.Images.Add(_context.Images
                .Where(image => image.ImageId == id)
                .Where(image => !image.IsDeleted)
                .Select(image => new Image
                {
                    BasePrice = image.BasePrice,
                    ImageData = image.ImageData
                })
                .SingleOrDefault());

        }
    }
}
