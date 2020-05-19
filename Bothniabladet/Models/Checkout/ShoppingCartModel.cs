using System;
using Bothniabladet.Data;
using System.ComponentModel.DataAnnotations;

namespace Bothniabladet.Models.Checkout
{
    public class ShoppingCartModel
    {
        public String ImagesStringData { get; set; } // Add images to view
        public Image Images { get; set; } // Populate model with images
        public ApplicationUser User { get; set; }
        public int Price { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Title must start with a capital letter")]
        [StringLength(40, ErrorMessage = "Title can be no more than 40 characters long")]
        [Display(Name = "Förnamn")]
        public string fName { get; set; }
        [Required]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$", ErrorMessage = "Title must start with a capital letter")]
        [StringLength(40, ErrorMessage = "Title can be no more than 40 characters long")]
        [Display(Name = "Efternamn")]
        public string lName { get; set; }


        public String Address { get; set; }
        public String PaymentMethod { get; set; }
        public bool Private { get; set; } // If false the the person is doing this for a company
        public double Discount { get; set; }


        public static ShoppingCartModel FromShoppingCart(ShoppingCart shoppingCart)
        {

            return new ShoppingCartModel
            {
                Images = shoppingCart.Image,
                User = shoppingCart.User,
                ImagesStringData = string.Format("data:image/jpg;base64,{0}", Convert.ToBase64String(shoppingCart.Image.Thumbnail)),
                Price = shoppingCart.Image.BasePrice
            };
        }

        public Data.DocumentOfSales toSale()
        {
            Data.DocumentOfSales salesDocument = new Data.DocumentOfSales
            {
                fName = fName,
                lName = lName,
                Address= Address,
                PaymentMethod = PaymentMethod,
                Private = Private,
            };

            return salesDocument;
        }
    }
}
