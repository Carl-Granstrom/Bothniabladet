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
        public int ShoppingCartId { get; set; }
        public bool Completed { get; set; } // Check if customer has a unfinished cart


        //Links
        public ICollection<ShoppingCartImage> ShoppingCartImages { get; set; } // Many to many realtionship with images
        public IdentityUser User { get; set; } // 1-1 A customer can only have one shoppingcart

    }
}
