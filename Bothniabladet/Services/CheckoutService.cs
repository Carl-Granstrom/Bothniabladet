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
        private readonly IHttpContextAccessor _httpContextAccessor;
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
        public ICollection<ShoppingCartModel> GetShoppingCart()
        {
            ApplicationUser user = _context.User
                .Where(user => user.Id == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Include(user => user.ShoppingCart)
                    .ThenInclude(sc => sc.Image)
                .SingleOrDefault();

            ICollection<ShoppingCartModel> shoppingCartModels = new List<ShoppingCartModel>();
            foreach (ShoppingCart sc in user.ShoppingCart)
            {
                shoppingCartModels.Add(new ShoppingCartModel()
                {
                    Images = sc.Image,
                    Price = sc.Image.BasePrice,
                    User = sc.User,
                    ImagesStringData = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(sc.Image.Thumbnail))
                });
            }

            return shoppingCartModels;
        }
        //Adds a item to the shoppingcart, if owned true item does not exist in shoppingcart
        public void AddItem(int id)
        {
            Image image = _context.Images.Find(id);
            ApplicationUser user = _context.User
                .Where(user => user.Id == _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier))
                .Include(user => user.ShoppingCart)
                .SingleOrDefault();
            if (user.ShoppingCart == null)
            {
                user.ShoppingCart = new List<ShoppingCart>();
            }
            bool inCart = false;
            foreach (ShoppingCart sc in user.ShoppingCart)
            {
                if (sc.ImageId == image.ImageId)
                {
                    inCart = true;
                }
            }
            if (!inCart)
            {
                user.ShoppingCart.Add(new ShoppingCart()
                {
                    Image = image,
                    User = user,
                    Owns = false
                });
            }

            _context.SaveChanges();
        }

        public void CompleteTransaction()
        {

        }
    }
}
