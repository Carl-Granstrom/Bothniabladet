using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Data
{
    public class ShoppingCart
    {
        //Variables
        public int CartId { get; set; }
        public bool Completed { get; set; } // Check if customer has a unfinished cart


        //Links
        public ICollection<Image> Images { get; set; } // All the images in the cart
        public String UserId { get; set; } // 1-1 A customer can only have one shoppingcart
        public String Name { get; set; }
        public String Email { get; set; }
    }
}
