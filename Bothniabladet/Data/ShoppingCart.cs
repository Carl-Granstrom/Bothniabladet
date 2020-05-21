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
        public bool Owns { get; set; } // Check if owner owns the image

        //Links
        public int ImageId { get; set; }
        public Image Image { get; set; }
        public String UserId { get; set; }
        public ApplicationUser User { get; set; } 

    }
}
