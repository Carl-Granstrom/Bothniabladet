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
                .Where(shoppingCart => shoppingCart.User.Id == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Where(shoppingCart => !shoppingCart.Completed)
                .Select(shoppingCart => new ShoppingCartModel
                {
                    Name = shoppingCart.User.UserName,
                    Email = shoppingCart.User.Email

                }).SingleOrDefault();

            return shoppingCartModel;
        }

        // Adds a new shoppingcart to a user
        public void NewShoppingCart()
        {

            var newShoppingCart = new ShoppingCart();

            //newShoppingCart.User.Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            newShoppingCart.Completed = false;
            newShoppingCart.User.Id = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(newShoppingCart);
            _context.SaveChanges();

        }

        public void AddItem(int id)
        {
            ShoppingCartModel shoppingCartModel = GetShoppingCart();
            shoppingCartModel.Images.Add(_context.ShoppingCart
                .Where(shoppingCart => shoppingCart.ImageId == id)
                .Where(shoppingCart => shoppingCart.Image.IsDeleted)
                .Select(shoppingCart => new Image
                {
                    BasePrice = shoppingCart.Image.BasePrice,
                    ImageData = shoppingCart.Image.ImageData
                })
                .SingleOrDefault());

        }
    }
}
