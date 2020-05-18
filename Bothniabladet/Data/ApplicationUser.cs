using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bothniabladet.Data
{
    //Custom data for the IdentityUser class, inherits IdentityUser
    public class ApplicationUser : IdentityUser
    {
        public ICollection<ShoppingCart> ShoppingCart { get; set; } // A users check to see if they own any books
    }
}
