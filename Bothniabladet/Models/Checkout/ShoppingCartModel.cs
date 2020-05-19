using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bothniabladet.Data;

namespace Bothniabladet.Models.Checkout
{
    public class ShoppingCartModel
    {
        public String ImagesStringData { get; set; } // Add images to view
        public Image Images { get; set; } // Populate model with images
        public ApplicationUser User { get; set; }
        public int Price { get; set; }

        public static ShoppingCartModel FromShoppingCart(ShoppingCart shoppingCart)
        {

            return new ShoppingCartModel
            {
                Images = shoppingCart.Image,
                User = shoppingCart.User,
                ImagesStringData = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(shoppingCart.Image.Thumbnail)),
                Price = shoppingCart.Image.BasePrice,
            };
        }
    }
}
