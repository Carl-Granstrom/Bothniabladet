﻿using Bothniabladet.Data;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Models.Checkout
{
    public class ShoppingCartModel
    {
        public ICollection<String> ImagesStringData { get; set; } // Add images to view
        public ICollection<Image> Images { get; set; } // Populate model with images
        public int Price { get; set; }

        public ApplicationUser User { get; set; }

        public static ShoppingCartModel ShoppingCart(ShoppingCart shoppingCart)
        {
            // Add all attached images if there are any
            List<String> thumbnail = new List<string>();
            var Price = 0;
            if (shoppingCart.Image != null)
            {
                foreach (Image image in s)
                {
                    thumbnail.Add(string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(image.Image.Thumbnail)));
                    Price += image.Image.BasePrice; // Could add the discount here
                }
            }
            return new ShoppingCartModel
            {
                //CartId = shoppingCart.ShoppingCartId,
                ImagesStringData = thumbnail,
                Price = Price,

            };
        }
    }
}
